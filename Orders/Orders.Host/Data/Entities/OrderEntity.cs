namespace Orders.Host.Data.Entities;

public class OrderEntity
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public decimal Total { get; set; }

    public string CreatedAt { get; set; } = null!;

    public List<ItemInfoEntity> Items { get; set; } = null!;
}