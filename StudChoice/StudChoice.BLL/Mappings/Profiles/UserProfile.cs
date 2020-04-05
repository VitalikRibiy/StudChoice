using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, IdentityUser<int>>();
            CreateMap<IdentityUser<int>,UserDTO>();
        }
    }
}
