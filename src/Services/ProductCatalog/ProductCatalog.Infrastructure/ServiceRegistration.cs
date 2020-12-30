using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Infrastructure.DataContexts;
using eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories;
using eShopWithReact.Services.ProductCatalog.Infrastructure.Settings;


namespace eShopWithReact.Services.ProductCatalog.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogDatabaseSetting>(x => configuration.GetSection(nameof(CatalogDatabaseSetting)).Bind(x));

            services.AddSingleton<ICatalogDatabaseSetting>(sp => sp.GetRequiredService<IOptions<CatalogDatabaseSetting>>().Value);

            services.AddTransient<ICatalogDbContext, CatalogDbContext>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        }
    }
}
