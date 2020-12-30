using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace eShopWithReact.Services.UserAuthentication.Core
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
