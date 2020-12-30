using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eShopWithReact.Services.UserAuthentication.Core.Entities;


namespace eShopWithReact.Services.UserAuthentication.Infrastructure.DataContexts
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }
}

