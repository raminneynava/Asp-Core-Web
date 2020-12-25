using AutoMapper;
using Data.Dtos;
using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MappingProfiles
{
  public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogCategory, BlogCategoryDto>().ReverseMap();
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<IdentityUser, UserListDto>().ReverseMap();
        }
    }
}
