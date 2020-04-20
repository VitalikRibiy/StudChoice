using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectDTO, Subject>();
            CreateMap<Subject, SubjectDTO>();
        }
    }
}
