using AutoMapper;
using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;
using ContactApp.Models;

namespace ContactApp.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Contact
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, 
                            opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
