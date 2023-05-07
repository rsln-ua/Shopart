namespace Catalog.Host.Models.Dtos;

public class ModelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public MakeDto Make { get; set; } = null!;
}