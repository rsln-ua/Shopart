using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class VehicleRepository : BaseRepository<VehicleRepository>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<VehicleRepository> logger)
        : base(dbContext, mapper, logger)
    {
    }

    public async Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        var item = new VehicleEntity
        {
            MakeId = makeId, ModelId = modelId, Vin = vin, Cylinders = cylinders, EngineSizeL = engineSizeL,
            Year = year, Mileage = mileage
        };
        var record = await DbContext.AddAsync(item);

        await DbContext.SaveChangesAsync();

        return record.Entity.Id;
    }

    public async Task<int?> Update(int id, int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage)
    {
        var oldItem = await DbContext.VehicleEntities.FirstOrDefaultAsync(el => el.Id == id);
        var newEntity = new VehicleEntity
        {
            Id = id, MakeId = makeId, ModelId = modelId, Vin = vin, Cylinders = cylinders, EngineSizeL = engineSizeL,
            Year = year, Mileage = mileage
        };

        if (oldItem == null)
        {
            return 0;
        }

        DbContext.Entry(oldItem).CurrentValues.SetValues(newEntity);
        await DbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int?> Remove(int id)
    {
        var entity = await DbContext.VehicleEntities.FirstOrDefaultAsync(el => el.Id == id);

        if (entity == null)
        {
            return 0;
        }

        DbContext.Entry(entity).State = EntityState.Deleted;
        await DbContext.SaveChangesAsync();

        return id;
    }

    public Task<VehicleEntity?> Get(int id)
    {
        return DbContext.VehicleEntities.Include(el => el.Make)
            .Include(el => el.Model)
            .FirstOrDefaultAsync(el => el.Id == id);
    }
}