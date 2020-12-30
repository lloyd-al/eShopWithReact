using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eShopWithReact.Services.UserAuthentication.Infrastructure.DataContexts;
using System;

namespace eShopWithReact.Services.UserAuthentication.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), options => {
                        options.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
                        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                     }));
        }
    }
}
