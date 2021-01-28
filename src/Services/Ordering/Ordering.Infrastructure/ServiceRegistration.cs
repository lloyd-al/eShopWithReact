using eShopWithReact.Services.Ordering.Core.Interfaces;
using eShopWithReact.Services.Ordering.Infrastructure.DataContexts;
using eShopWithReact.Services.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopWithReact.Services.Ordering.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Singleton);
            // we made singleton this in order to resolve in mediatR when consuming Rabbit

            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient<IOrderRepository, OrderRepository>();
            // we made transient this in order to resolve in mediatR when consuming Rabbit
        }
    }
}
