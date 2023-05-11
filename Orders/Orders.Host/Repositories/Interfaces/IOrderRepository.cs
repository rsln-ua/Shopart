using Orders.Host.Data.Entities;

namespace Orders.Host.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<int?> Add(string userId, decimal total);
    Task<int?> Update(int id, string userId, decimal total);
    Task<int?> Remove(int id);
    Task<OrderEntity?> Get(int id);
}