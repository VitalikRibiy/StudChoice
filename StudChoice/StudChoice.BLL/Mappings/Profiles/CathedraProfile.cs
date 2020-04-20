using AutoMapper;
using StudChoice.BLL.DTOs;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class CathedraProfile : Profile
    {
        public CathedraProfile()
        {
            CreateMap<CathedraDTO, Cathedra>();
            CreateMap<Cathedra, CathedraDTO>();
        }
    }
}
