using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Responses;

public class GetBasketItemResponse
{
    public BasketItemDto Data { get; set; } = null!;
}