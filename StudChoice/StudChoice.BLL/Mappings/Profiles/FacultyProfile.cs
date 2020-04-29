using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<FacultyDTO, Faculty>();
            CreateMap<Faculty, FacultyDTO>();
        }
    }
}
