using AutoMapper;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using eShopWithReact.Services.Ordering.Application.Handlers;

namespace eShopWithReact.Services.Ordering.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add MediatR
            services.AddMediatR(typeof(CheckoutOrderHandler).GetTypeInfo().Assembly);
        }
    }
}
