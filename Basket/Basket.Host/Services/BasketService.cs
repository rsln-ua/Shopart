using Basket.Host.Configurations;
using Basket.Host.Models.Dtos;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly BasketConfig _config;
    private readonly IInternalHttpClientService _httpClientService;

    public BasketService(ICacheService cacheService, IInternalHttpClientService httpClientService, IOptions<BasketConfig> config)
    {
        _cacheService = cacheService;
        _httpClientService = httpClientService;
        _config = config.Value;
    }

    public async Task<string> Set(string userId, List<BaseBasketItemDto> items)
    {
        var preparedItems = new List<BasketItemDto>();

        foreach (var el in items)
        {
            var item = await GetItem(el.ItemId);
            item.Quantity = el.Quantity;

            preparedItems.Add(item);
        }

        await _cacheService.Set(userId, new BasketDto() { Items = preparedItems, Total = GetTotal(preparedItems) });

        return userId;
    }

    public Task<BasketDto> Get(string userId)
    {
        return _cacheService.Get<BasketDto>(userId);
    }

    private static decimal GetTotal(IEnumerable<BasketItemDto> items)
    {
        return items.Aggregate(0m, (acc, el) => acc + (el.Price * el.Quantity));
    }

    private async Task<BasketItemDto> GetItem(int id)
    {
        // TODO: why too long??
        var response = await _httpClientService.SendAsync<GetBasketItemResponse, object>(_config.CatalogUrl + "/api/v1/Vehicle/GetItemInfo", HttpMethod.Post, new
        {
            id = id
        });

        return response.Data;
    }
}