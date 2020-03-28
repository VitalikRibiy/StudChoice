using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.ViewModels;

namespace StudChoice.BLL.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<IdentityUser<int>, UserVM>()
                .ForMember(dto => dto.Role, prop => prop.Ignore())
                .ReverseMap();
        }
    }
}