using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eShopWithReact.Services.UserAuthentication.Core.Entities;
using eShopWithReact.Services.UserAuthentication.Core.Helpers;
using System;

namespace eShopWithReact.Services.UserAuthentication.Infrastructure.DataContexts
{
    public class UserDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if (!roleManager.RoleExistsAsync(UserRole.Admin).Result)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                await roleManager.CreateAsync(new IdentityRole(UserRole.User));
            }

            //Seed Default User
            var defaultUser = new ApplicationUser {
                FirstName = "Lloyd",
                LastName = "Almeida",
                UserName = "lloyd@yahoo.com",
                Email = "lloyd@yahoo.com", 
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true,
                SecurityStamp = String.Concat(Array.ConvertAll(Guid.NewGuid().ToByteArray(), b => b.ToString("X2")))
        };

            if (userManager.FindByEmailAsync(defaultUser.Email).Result == null)
            {
                IdentityResult result = userManager.CreateAsync(defaultUser, "Password@1234").Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, UserRole.Admin);
                }
                    
            }
        }
    }
}
