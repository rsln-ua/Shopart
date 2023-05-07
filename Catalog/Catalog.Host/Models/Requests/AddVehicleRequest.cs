namespace Catalog.Host.Models.Requests;

public class AddVehicleRequest
{
    public int MakeId { get; set; }

    public int ModelId { get; set; }

    public string Vin { get; set; } = null!;

    public int Year { get; set; }

    public int Cylinders { get; set; }

    public float EngineSizeL { get; set; }

    public float Mileage { get; set; }
}