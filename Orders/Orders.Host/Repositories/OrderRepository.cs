using Orders.Host.Data;
using Orders.Host.Data.Entities;
using Orders.Host.Repositories.Interfaces;

namespace Orders.Host.Repositories;

public class OrderRepository : BaseRepository<OrderRepository>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<OrderRepository> logger)
        : base(dbContext, mapper, logger)
    {
    }

    public async Task<int?> Add(string userId, decimal total)
    {
        var item = new OrderEntity()
        {
            UserId = userId, Total = total, CreatedAt = DateTime.Now.ToString()
        };
        var record = await DbContext.AddAsync(item);

        await DbContext.SaveChangesAsync();

        return record.Entity.Id;
    }

    public async Task<int?> Update(int id, string userId, decimal total)
    {
        var oldItem = await DbContext.OrderEntities.FirstOrDefaultAsync(el => el.Id == id);
        var newEntity = new OrderEntity()
        {
            Id = id, UserId = userId, Total = total, CreatedAt = oldItem!.CreatedAt
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
        var entity = await DbContext.OrderEntities.FirstOrDefaultAsync(el => el.Id == id);

        if (entity == null)
        {
            return 0;
        }

        DbContext.Entry(entity).State = EntityState.Deleted;
        await DbContext.SaveChangesAsync();

        return id;
    }

    public Task<OrderEntity?> Get(int id)
    {
        return DbContext.OrderEntities.Include(el => el.Items).FirstOrDefaultAsync(el => el.Id == id);
    }
}