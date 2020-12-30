using System;
using AutoMapper;
using eShopWithReact.Services.UserAuthentication.Core.Entities;
using eShopWithReact.Services.UserAuthentication.Core.Models;

namespace eShopWithReact.Services.UserAuthentication.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, AccountResponse>();

            CreateMap<ApplicationUser, AuthenticateResponse>();

            CreateMap<RegisterRequest, ApplicationUser>();

            CreateMap<UpdateRequest, ApplicationUser>();
        }
    }
}
