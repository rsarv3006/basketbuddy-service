using System.Net;
using ApiAlerts.Common.Models;
using ApiAlerts.Common.Services;
using BasketBuddy.Api.Dto;
using BasketBuddy.Api.Services;
using BasketBuddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketBuddy.Api.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class ShareController : ControllerBase
{
  private readonly ShareService _shareService;
  private readonly AlertService _alertService;

  public ShareController(ShareService shareService, AlertService alertService)
  {
    _shareService = shareService ?? throw new ArgumentNullException(nameof(shareService));
    alertService.Activate("");
    _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
  }

  [HttpPost]
  [Route("create")]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> CreateShareAsync(ShareCreateDto dto)
  {
    var model = await _shareService.CreateShare(dto);
    List<string> tags = new List<string>
            { "basketbuddy", "share", "create" };

    var alert = new ApiAlert(messageText: "Share Created", tags, "/api/v1/Share/create");
    _alertService.PublishAlert(alert);
    return new CreatedResult($"/api/v1/Share/{model.ShareCode}", model);
  }

  [HttpGet]
  [Route("{shareCode}")]
  [ProducesResponseType(typeof(Share), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> GetShareAsync(string shareCode)
  {
    try
    {
      var share = await _shareService.GetShare(shareCode);

      List<string> tags = new List<string>
        { "basketbuddy", "share", "get" };

      var alert = new ApiAlert(messageText: "Share Get", tags, "/api/v1/Share/");
      _alertService.PublishAlert(alert);

      return Ok(share);
    }
    catch (Exception ex) when (ex.Message.Contains("Share not found"))
    {
      return BadRequest("Share not found");
    }
    catch (Exception ex) when (ex.Message.Contains("Share has expired"))
    {
      return BadRequest("Share has expired");
    }
  }

  [HttpDelete]
  [Route("delete-expired")]
  [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task DeleteAllExpiredShares()
  {
    await _shareService.DeleteAllExpiredShares();
  }
}
