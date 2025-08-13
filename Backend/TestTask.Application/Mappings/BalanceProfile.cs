
using AutoMapper;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Mappings
{
    public class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            CreateMap<Balance, BalanceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ResourceDTO, opt => opt.MapFrom(src => src.Resource)) 
                .ForMember(dest => dest.UnitDTO, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<BalanceDTO, Balance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Resource, opt => opt.MapFrom(src => src.ResourceDTO))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.UnitDTO))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
