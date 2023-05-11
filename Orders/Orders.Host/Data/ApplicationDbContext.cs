using Orders.Host.Data.Entities;
using Orders.Host.Data.EntityConfigurations;

namespace Orders.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ItemInfoEntity> ItemInfoEntities { get; set; } = null!;
    public DbSet<OrderEntity> OrderEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.ApplyConfiguration(new ItemInfoEntityConfiguration());
    }
}