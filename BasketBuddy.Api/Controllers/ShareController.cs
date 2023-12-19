using System.Net;
using System.Text.Json;
using BasketBuddy.Api.Dto;
using BasketBuddy.Api.Services;
using BasketBuddy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketBuddy.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<Share>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateShareAsync(ShareCreateDto dto)
    {
        var model = await _shareService.CreateShare(dto);

        return Ok(model);
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
}