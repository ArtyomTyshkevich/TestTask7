using AutoMapper;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Mapping
{
    public class OutgoingDocumentProfile : Profile
    {
        public OutgoingDocumentProfile()
        {
            CreateMap<OutgoingDocument, OutgoingDocumentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.ClientDTO, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.OutgoingResourcesDTO, opt => opt.MapFrom(src => src.OutgoingResources));

            CreateMap<OutgoingDocumentDTO, OutgoingDocument>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.ClientDTO))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.OutgoingResources, opt => opt.MapFrom(src => src.OutgoingResourcesDTO));
        }
    }
}
