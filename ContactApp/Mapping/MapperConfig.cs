using AutoMapper;
using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;

namespace ContactApp.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Contact
            CreateMap<ContactDTO, Contact>().ReverseMap();
        }
    }
}
