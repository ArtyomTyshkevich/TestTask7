using AutoMapper;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Mapping
{
    public class OutgoingResourceProfile : Profile
    {
        public OutgoingResourceProfile()
        {
            CreateMap<OutgoingResource, OutgoingResourceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ResourceDTO, opt => opt.MapFrom(src => src.Resource))
                .ForMember(dest => dest.UnitDTO, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<OutgoingResourceDTO, OutgoingResource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Resource, opt => opt.MapFrom(src => src.ResourceDTO))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.UnitDTO))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
