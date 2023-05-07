using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;

namespace Catalog.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleEntity> VehicleEntities { get; set; } = null!;
    public DbSet<ModelEntity> ModelEntities { get; set; } = null!;
    public DbSet<MakeEntity> MakeEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ModelEntityConfiguration());
        builder.ApplyConfiguration(new MakeEntityConfiguration());
        builder.ApplyConfiguration(new VehicleEntityConfiguration());
    }
}
