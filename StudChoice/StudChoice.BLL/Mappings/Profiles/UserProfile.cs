using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using StudChoice.DAL.Models;

namespace StudChoice.BLL.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
