using Orders.Host.Data;
using Orders.Host.Data.Entities;
using Orders.Host.Repositories.Interfaces;

namespace Orders.Host.Repositories;

public class ItemInfoRepository : BaseRepository<ItemInfoRepository>, IItemInfoRepository
{
    public ItemInfoRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<ItemInfoRepository> logger)
        : base(dbContext, mapper, logger)
    {
    }

    public async Task<int?> Add(int orderId, decimal price, int quantity, string name)
    {
        var item = new ItemInfoEntity
        {
            OrderId = orderId, Price = price, Name = name, Quantity = quantity,
        };
        var record = await DbContext.AddAsync(item);

        await DbContext.SaveChangesAsync();

        return record.Entity.Id;
    }

    public async Task<int?> Update(int id, int orderId, decimal price, int quantity, string name)
    {
        var oldItem = await DbContext.ItemInfoEntities.FirstOrDefaultAsync(el => el.Id == id);
        var newEntity = new ItemInfoEntity
        {
            Id = id, OrderId = orderId, Price = price, Name = name, Quantity = quantity,
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
        var entity = await DbContext.ItemInfoEntities.FirstOrDefaultAsync(el => el.Id == id);

        if (entity == null)
        {
            return 0;
        }

        DbContext.Entry(entity).State = EntityState.Deleted;
        await DbContext.SaveChangesAsync();

        return id;
    }

    public Task<ItemInfoEntity?> Get(int id)
    {
        return DbContext.ItemInfoEntities.FirstOrDefaultAsync(el => el.Id == id);
    }
}