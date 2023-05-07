using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

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
}