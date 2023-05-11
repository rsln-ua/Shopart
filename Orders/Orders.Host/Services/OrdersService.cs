using Orders.Host.Data;
using Orders.Host.Models.Dtos;
using Orders.Host.Repositories.Interfaces;
using Orders.Host.Services.Interfaces;

namespace Orders.Host.Services;

public class OrdersService : BaseDataService<ApplicationDbContext>, IOrdersService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemInfoService _itemInfoService;

    public OrdersService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<OrdersService> logger, IMapper mapper, IOrderRepository orderRepository, IItemInfoService itemInfoService)
        : base(dbContextWrapper, logger, mapper)
    {
        _orderRepository = orderRepository;
        _itemInfoService = itemInfoService;
    }

    public async Task<int?> Add(string userId, List<BaseItemInfoDto> items)
    {
        var orderId = await ExecuteSafeAsync(() => _orderRepository.Add(userId, 0));

        if (orderId == null)
        {
            return null;
        }

        var total = 0m;

        foreach (var it in items)
        {
            var itemId = await _itemInfoService.Add((int)orderId, it.ItemId, it.Quantity);

            if (itemId == null)
            {
                continue;
            }

            var item = await _itemInfoService.Get((int)itemId);
            total += item == null ? 0 : item.Price * item.Quantity;
        }

        return await ExecuteSafeAsync(() => _orderRepository.Update((int)orderId, userId, total));
    }

    public async Task<int?> Remove(int id)
    {
        var item = await _orderRepository.Get(id);

        if (item == null)
        {
            return null;
        }

        foreach (var el in item.Items)
        {
            await _itemInfoService.Remove(el.Id);
        }

        return await ExecuteSafeAsync(() => _orderRepository.Remove(id));
    }

    public async Task<OrderDto?> Get(int id)
    {
        var item = await _orderRepository.Get(id);

        if (item == null)
        {
            return null;
        }

        return Mapper.Map<OrderDto>(item);
    }
}