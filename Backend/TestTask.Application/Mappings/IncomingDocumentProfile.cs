using AutoMapper;
using TestTask.Application.DTOs.Warehouse;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.Mapping
{
    public class IncomingDocumentProfile : Profile
    {
        public IncomingDocumentProfile()
        {
            CreateMap<IncomingDocument, IncomingDocumentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.IncomingResourcesDTO, opt => opt.MapFrom(src => src.IncomingResources));

            CreateMap<IncomingDocumentDTO, IncomingDocument>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.IncomingResources, opt => opt.MapFrom(src => src.IncomingResourcesDTO));
        }
    }
}
