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
    }
}