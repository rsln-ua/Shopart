using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

// [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
// [Scope("catalog.catalogitem")]
[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class VehicleController : ControllerBase
{
    private readonly ILogger<VehicleController> _logger;
    private readonly IVehicleService _vehicleService;

    public VehicleController(
        ILogger<VehicleController> logger,
        IVehicleService vehicleService)
    {
        _logger = logger;
        _vehicleService = vehicleService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddVehicleRequest request)
    {
        var result = await _vehicleService.Add(request.MakeId, request.ModelId, request.Vin, request.Year, request.Cylinders, request.EngineSizeL, request.Mileage);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateVehicleRequest request)
    {
        var result = await _vehicleService.Update(request.Id, request.MakeId, request.ModelId, request.Vin, request.Year, request.Cylinders, request.EngineSizeL, request.Mileage);
        return Ok(new UpdateItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveVehicleRequest request)
    {
        var result = await _vehicleService.Remove(request.Id);
        return Ok(new UpdateItemResponse<int?>() { Id = result });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetItemResponse<VehicleDto?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _vehicleService.Get(id);
        return Ok(new GetItemResponse<VehicleDto?>() { Data = result });
    }
}