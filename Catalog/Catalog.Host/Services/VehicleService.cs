using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class VehicleService : BaseDataService<ApplicationDbContext>, IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, IMapper mapper, IVehicleRepository vehicleRepository)
        : base(dbContextWrapper, logger, mapper)
    {
        _vehicleRepository = vehicleRepository;
    }

    public Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        return ExecuteSafeAsync(() => _vehicleRepository.Add(makeId, modelId, vin, year, cylinders, engineSizeL, mileage));
    }

    public Task<int?> Update(int id, int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        return ExecuteSafeAsync(() => _vehicleRepository.Update(id, makeId, modelId, vin, year, cylinders, engineSizeL, mileage));
    }

    public Task<int?> Remove(int id)
    {
        return ExecuteSafeAsync(() => _vehicleRepository.Remove(id));
    }

    public async Task<VehicleDto?> Get(int id)
    {
        var entity = await _vehicleRepository.Get(id);
        return Mapper.Map<VehicleDto>(entity);
    }
}