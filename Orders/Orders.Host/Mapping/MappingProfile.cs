using Orders.Host.Data.Entities;
using Orders.Host.Models.Dtos;

namespace Orders.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ItemInfoEntity, ItemInfoDto>();
        CreateMap<OrderEntity, OrderDto>();
    }
}