using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eShopWithReact.Common.EventBusRabbitMQ;
using eShopWithReact.Common.EventBusRabbitMQ.Producer;
using eShopWithReact.Common.Infrastructure.Extensions;
using RabbitMQ.Client;

namespace eShop.Basket.Api.Extensions
{
    public static class ServiceExtension
    {
        public static object Configuration { get; private set; }

        public static void ConfigureExtensions(this IServiceCollection services)
        {
            services.ConfigureApiVersioning();
            services.ConfigureSwagger("Basket Api");
            services.ConfigureLoggerService();
            services.ConfigureMailService();
        }

        public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
                {
                    factory.UserName = configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
                {
                    factory.Password = configuration["EventBus:Password"];
                }

                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
        }

    }
}
