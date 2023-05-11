using Orders.Host.Data.Entities;

namespace Orders.Host.Repositories.Interfaces;

public interface IItemInfoRepository
{
    Task<int?> Add(int orderId, decimal price, int quantity, string name);
    Task<int?> Update(int id, int orderId, decimal price, int quantity, string name);
    Task<int?> Remove(int id);
    Task<ItemInfoEntity?> Get(int id);
}