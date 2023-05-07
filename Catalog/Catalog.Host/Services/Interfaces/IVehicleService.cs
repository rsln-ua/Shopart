using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface IVehicleService
{
    Task<int?> Add(int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage);
    Task<int?> Update(int id, int makeId, int modelId, string vin, int year, int cylinders, float engineSizeL, float mileage);
    Task<int?> Remove(int id);
    Task<VehicleDto?> Get(int id);
}