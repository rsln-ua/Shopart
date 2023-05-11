using Orders.Host.Models.Dtos;

namespace Orders.Host.Models.Requests;

public class AddOrderRequest
{
    public List<BaseItemInfoDto> Items { get; set; } = null!;
}