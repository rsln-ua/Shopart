using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities;

public class VehicleEntity
{
    public int Id { get; set; }

    public int MakeId { get; set; }
    public MakeEntity Make { get; set; } = null!;

    public int ModelId { get; set; }
    public ModelEntity Model { get; set; } = null!;

    [MaxLength(10)]
    [MinLength(3)]
    public string Vin { get; set; } = null!;

    public int Year { get; set; }

    public int Cylinders { get; set; }

    public float EngineSizeL { get; set; }

    public float Mileage { get; set; }
}