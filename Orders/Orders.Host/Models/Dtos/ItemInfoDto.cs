namespace Orders.Host.Models.Dtos;

public class ItemInfoDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Name { get; set; } = null!;
}