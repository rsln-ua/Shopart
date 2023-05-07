namespace Catalog.Host.Services.Interfaces;

public interface IVehicleService
{
    Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage);
}