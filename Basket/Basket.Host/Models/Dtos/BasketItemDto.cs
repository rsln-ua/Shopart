namespace Basket.Host.Models.Dtos;

public class BasketItemDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
}