using AutoMapper;
using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));

        CreateMap<ClientDTO, Client>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>
                (src.Id == null || src.Id == Guid.Empty) ? Guid.NewGuid() : src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
    }
}
