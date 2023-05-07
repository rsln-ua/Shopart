using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.MakeEntities.Any())
        {
            await context.MakeEntities.AddRangeAsync(GetCatalogMakes());

            await context.SaveChangesAsync();
        }

        if (!context.ModelEntities.Any())
        {
            await context.ModelEntities.AddRangeAsync(GetCatalogModels());

            await context.SaveChangesAsync();
        }

        if (!context.VehicleEntities.Any())
        {
            await context.VehicleEntities.AddRangeAsync(GetCatalogVehicles());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<MakeEntity> GetCatalogMakes()
    {
        return new List<MakeEntity>()
        {
            new MakeEntity() { Name = "Toyota" },
            new MakeEntity() { Name = "Honda" },
        };
    }

    private static IEnumerable<ModelEntity> GetCatalogModels()
    {
        return new List<ModelEntity>()
        {
            new ModelEntity() { Name = "Camry", MakeId = 0 },
            new ModelEntity() { Name = "Corolla", MakeId = 0 },
            new ModelEntity() { Name = "Civic", MakeId = 1 },
            new ModelEntity() { Name = "Accord", MakeId = 1 },
        };
    }

    private static IEnumerable<VehicleEntity> GetCatalogVehicles()
    {
        return new List<VehicleEntity>()
        {
            new VehicleEntity { MakeId = 1, ModelId = 2, Cylinders = 4, Vin = "4Y1SL65848Z411439", Year = 2016, Mileage = 66000, EngineSizeL = 1.5f },
        };
    }
}