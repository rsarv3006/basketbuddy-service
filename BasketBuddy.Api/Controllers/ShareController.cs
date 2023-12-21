using System.Net;
using BasketBuddy.Api.Dto;
using BasketBuddy.Api.Services;
using BasketBuddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketBuddy.Api.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class ShareController: ControllerBase
{
    private readonly ShareService _shareService;
    
    public ShareController(ShareService shareService)
    {
        _shareService = shareService ?? throw new ArgumentNullException(nameof(shareService));
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateShareAsync(ShareCreateDto dto)
    {
        var model = await _shareService.CreateShare(dto);

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