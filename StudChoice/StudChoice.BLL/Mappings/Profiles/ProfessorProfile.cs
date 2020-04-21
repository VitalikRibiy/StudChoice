using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<ProfessorDTO, Professor>();
            CreateMap<Professor, ProfessorDTO>();
        }
    }
}
