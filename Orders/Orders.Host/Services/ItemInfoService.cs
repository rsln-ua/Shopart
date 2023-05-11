using Orders.Host.Configurations;
using Orders.Host.Data;
using Orders.Host.Data.Entities;
using Orders.Host.Models.Dtos;
using Orders.Host.Models.Response;
using Orders.Host.Repositories.Interfaces;
using Orders.Host.Services.Interfaces;

namespace Orders.Host.Services;

public class ItemInfoService : BaseDataService<ApplicationDbContext>, IItemInfoService
{
    private readonly IItemInfoRepository _itemInfoRepository;

    private readonly OrdersConfig _config;
    private readonly IInternalHttpClientService _httpClientService;

    public ItemInfoService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<ItemInfoService> logger, IMapper mapper, IItemInfoRepository itemInfoRepository, IInternalHttpClientService httpClientService, IOptionsSnapshot<OrdersConfig> config)
        : base(dbContextWrapper, logger, mapper)
    {
        _itemInfoRepository = itemInfoRepository;

        _config = config.Value;
        _httpClientService = httpClientService;
    }

    public async Task<int?> Add(int orderId, int id, int quantity)
    {
        var item = await GetItem(id);
        item.Quantity = quantity;

        return await ExecuteSafeAsync(() => _itemInfoRepository.Add(orderId, item.Price, item.Quantity, item.Name));
    }

    public Task<int?> Remove(int id)
    {
        return ExecuteSafeAsync(() => _itemInfoRepository.Remove(id));
    }

    public Task<ItemInfoEntity?> Get(int id)
    {
        return _itemInfoRepository.Get(id);
    }

    private async Task<ItemInfoDto> GetItem(int id)
    {
        var response = await _httpClientService.SendAsync<GetItemResponse<ItemInfoDto>, object>(_config.CatalogUrl + "/api/v1/Vehicle/GetItemInfo", HttpMethod.Post, new
        {
            id
        });

        return response.Data;
    }
}