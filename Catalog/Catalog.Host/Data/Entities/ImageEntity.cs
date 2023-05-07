namespace Catalog.Host.Data.Entities;

public class ImageEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Uri { get; set; } = null!;
}