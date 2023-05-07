namespace Catalog.Host.Models.Dtos;

public class ImageDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Uri { get; set; } = null!;
}