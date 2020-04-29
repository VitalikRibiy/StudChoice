using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Role, prop => prop.Ignore())
                .ReverseMap();

            CreateMap<FacultyDTO, Faculty>()
                .ForMember(ent => ent.Cathedras, prop => prop.Ignore())
                .ReverseMap();
        }
    }
}