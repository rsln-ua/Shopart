using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

// [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;
    private readonly IBasketService _basketService;

    public BasketController(
        ILogger<BasketController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    public async Task<IActionResult> Set(SetRequest data)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "swagger";
        return Ok(await _basketService.Set(basketId!, data.Data));
    }

    [HttpPost]
    public async Task<IActionResult> Get()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "swagger";
        var response = await _basketService.Get(basketId!);
        return Ok(response);
    }
}