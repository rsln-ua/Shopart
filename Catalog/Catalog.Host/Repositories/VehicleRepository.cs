using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<VehicleRepository> _logger;

    public VehicleRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<VehicleRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        var item = new VehicleEntity
        {
            MakeId = makeId, ModelId = modelId, Vin = vin, Cylinders = cylinders, EngineSizeL = engineSizeL,
            Year = year, Mileage = mileage
        };
        var record = await _dbContext.AddAsync(item);

        await _dbContext.SaveChangesAsync();

        return record.Entity.Id;
    }

    public Task<int?> Update(int id, int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        throw new NotImplementedException();
    }

    public Task<int?> Remove(int id)
    {
        throw new NotImplementedException();
    }
}