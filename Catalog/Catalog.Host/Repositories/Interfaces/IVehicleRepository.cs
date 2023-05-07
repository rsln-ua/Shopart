namespace Catalog.Host.Repositories.Interfaces;

public interface IVehicleRepository
{
    Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage);
    Task<int?> Update(int id, int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage);
    Task<int?> Remove(int id);
}