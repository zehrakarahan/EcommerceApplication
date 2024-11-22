using AutoMapper;
using EcommerceApplication.Application.Request;
using EcommerceApplication.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Persistance.Mapper.UserMapping
{
    public class UserMapping :Profile
    {
        public UserMapping() {
            
            CreateMap<ApplicationUser, LoginRequest>().ReverseMap();
           
            CreateMap<ApplicationUser, CreateUserRequest>().ReverseMap();

        }

    }
}
