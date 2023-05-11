namespace Orders.Host.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public decimal Total { get; set; }

    public string CreatedAt { get; set; } = null!;

    public List<ItemInfoDto> Items { get; set; } = null!;
}