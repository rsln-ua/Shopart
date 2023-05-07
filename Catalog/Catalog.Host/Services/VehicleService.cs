using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class VehicleService : BaseDataService<ApplicationDbContext>, IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IVehicleRepository vehicleRepository)
        : base(dbContextWrapper, logger)
    {
        _vehicleRepository = vehicleRepository;
    }

    public Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        return ExecuteSafeAsync(() => _vehicleRepository.Add(makeId, modelId, vin, year, cylinders, engineSizeL, mileage));
    }
}