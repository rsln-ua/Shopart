using Orders.Host.Data.Entities;

namespace Orders.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.OrderEntities.Any())
        {
            await context.OrderEntities.AddRangeAsync(GetCatalogOrders());

            await context.SaveChangesAsync();
        }

        if (!context.ItemInfoEntities.Any())
        {
            await context.ItemInfoEntities.AddRangeAsync(GetCatalogItemsInfo());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<OrderEntity> GetCatalogOrders()
    {
        return new List<OrderEntity>()
        {
            new OrderEntity() { UserId = "swagger", CreatedAt = DateTime.Now.ToString(), Total = 100500m },
            new OrderEntity() { UserId = "swagger", CreatedAt = DateTime.Now.ToString(), Total = 200500m },
            new OrderEntity() { UserId = "swagger", CreatedAt = DateTime.Now.ToString(), Total = 2000m },
        };
    }

    private static IEnumerable<ItemInfoEntity> GetCatalogItemsInfo()
    {
        return new List<ItemInfoEntity>()
        {
            new ItemInfoEntity { Name = "Toyota Avalon 2020", Price = 100500m, Quantity = 1, OrderId = 1 },
            new ItemInfoEntity { Name = "Honda Civic 2018", Price = 100500m, Quantity = 1, OrderId = 2 },
            new ItemInfoEntity { Name = "Subaru Outback 2015", Price = 100000m, Quantity = 1, OrderId = 2 },
            new ItemInfoEntity { Name = "Saturn SL2 2000", Price = 1000m, Quantity = 2, OrderId = 3 },
        };
    }
}