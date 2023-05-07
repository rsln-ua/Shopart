namespace Catalog.Host.Data.Entities;

public class ModelEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int MakeId { get; set; }
    public MakeEntity Make { get; set; } = null!;
}