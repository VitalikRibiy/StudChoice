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
                .ForMember(dto => dto.FacultyName, prop => prop.Ignore())
                .ForMember(dto => dto.CathedraName, prop => prop.Ignore())
                .ForMember(dto => dto.Dv1IName, prop => prop.Ignore())
                .ForMember(dto => dto.Dv2IName, prop => prop.Ignore())
                .ForMember(dto => dto.Dvvs1Name, prop => prop.Ignore())
                .ForMember(dto => dto.Dvvs2Name, prop => prop.Ignore())
                .ReverseMap();

            CreateMap<FacultyDTO, Faculty>()
                .ForMember(ent => ent.Cathedras, prop => prop.Ignore())
                .ReverseMap();
        }
    }
}