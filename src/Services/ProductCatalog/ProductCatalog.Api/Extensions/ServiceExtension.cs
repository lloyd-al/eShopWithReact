using Microsoft.Extensions.DependencyInjection;
using eShopWithReact.Common.Infrastructure.Extensions;

namespace eShopWithReact.Services.ProductCatalog.Api.Extensions
{ 
    public static class ServiceExtension
    {
        public static void ConfigureExtensions(this IServiceCollection services)
        {
            services.ConfigureApiVersioning();
            services.ConfigureSwagger("Catalog Api");
            services.ConfigureLoggerService();
            services.ConfigureMailService();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
    }
}
