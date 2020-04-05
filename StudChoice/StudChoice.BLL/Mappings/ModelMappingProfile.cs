using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;

namespace StudChoice.BLL.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<IdentityUser<int>, UserDTO>()
                .ForMember(dto => dto.Role, prop => prop.Ignore())
                .ReverseMap();
        }
    }
}