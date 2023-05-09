using System.ComponentModel.DataAnnotations;
using Basket.Host.Models.Dtos;

namespace Basket.Host.Models;

public class SetRequest
{
    [Required]
    public List<BaseBasketItemDto> Data { get; set; } = null!;
}