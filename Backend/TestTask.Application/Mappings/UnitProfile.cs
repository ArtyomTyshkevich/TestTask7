using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;

namespace TestTask.Application.Mapping
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<Unit, UnitDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));

            CreateMap<UnitDTO, Unit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>
                    (src.Id == null || src.Id == Guid.Empty) ? Guid.NewGuid() : src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
        }
    }
}