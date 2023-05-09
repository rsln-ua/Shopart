namespace Basket.Host.Models.Dtos;

public class BasketDto
{
    public List<BasketItemDto> Items { get; set; } = null!;
    public decimal Total { get; set; }
}