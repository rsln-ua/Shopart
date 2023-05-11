using Orders.Host.Models.Dtos;
using Orders.Host.Models.Requests;
using Orders.Host.Models.Response;
using Orders.Host.Services.Interfaces;

namespace Orders.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersService _ordersService;

    public OrdersController(
        ILogger<OrdersController> logger,
        IOrdersService ordersService)
    {
        _logger = logger;
        _ordersService = ordersService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddOrderRequest request)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "swagger";
        var result = await _ordersService.Add(userId, request.Items);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    public async Task<IActionResult> Get(GetItemResponse<int> request)
    {
        var result = await _ordersService.Get(request.Data);
        return Ok(new GetItemResponse<OrderDto?>() { Data = result });
    }
}