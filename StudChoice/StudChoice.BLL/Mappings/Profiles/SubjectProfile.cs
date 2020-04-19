using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<ProfessorDTO, Subject>();
            CreateMap<Subject, ProfessorDTO>();
        }
    }
}
