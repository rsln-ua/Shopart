using Orders.Host.Data.Entities;

namespace Orders.Host.Services.Interfaces;

public interface IItemInfoService
{
    Task<int?> Add(int orderId, int id, int quantity);
    Task<int?> Remove(int id);
    Task<ItemInfoEntity?> Get(int id);
}