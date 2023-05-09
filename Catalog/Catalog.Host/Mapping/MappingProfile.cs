using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleEntity, VehicleDto>();
        CreateMap<ModelEntity, ModelDto>();
        CreateMap<MakeEntity, MakeDto>();
        CreateMap<VehicleEntity, BasketItemDto>().ForMember(
            p => p.Name, opt => opt.MapFrom(el => $"{el.Make.Name} {el.Model.Name} {el.Year}"));
    }
}