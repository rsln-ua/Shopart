namespace Catalog.Host.Models.Dtos;

public class VehicleDto
{
    public int Id { get; set; }

    public MakeDto Make { get; set; } = null!;

    public ModelDto Model { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public int Year { get; set; }

    public int Cylinders { get; set; }

    public float EngineSizeL { get; set; }

    public float Mileage { get; set; }
}