using System.Net;
using System.Text.Json;
using BasketBuddy.Api.Dto;
using BasketBuddy.Api.Services;
using BasketBuddy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketBuddy.Api.Controllers;

[Route("api/v1/[controller]")]
public class ShareController: ControllerBase
{
    private readonly ShareService _shareService;
    
    public ShareController(ShareService shareService)
    {
        _shareService = shareService ?? throw new ArgumentNullException(nameof(shareService));
    }

    // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(IEnumerable<Share>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateShareAsync([FromBody] JsonDocument data)
    {
        var model = await _shareService.CreateShare(new ShareCreateDto(data));

        return Ok(model);
    }
}