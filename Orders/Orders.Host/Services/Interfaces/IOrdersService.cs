using Orders.Host.Data.Entities;
using Orders.Host.Models.Dtos;

namespace Orders.Host.Services.Interfaces;

public interface IOrdersService
{
    Task<int?> Add(string userId, List<BaseItemInfoDto> items);
    Task<int?> Remove(int id);
    Task<OrderDto?> Get(int id);
}