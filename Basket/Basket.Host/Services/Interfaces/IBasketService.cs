using Basket.Host.Models.Dtos;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task<string> Set(string userId, List<BaseBasketItemDto> items);
    Task<BasketDto> Get(string userId);
}