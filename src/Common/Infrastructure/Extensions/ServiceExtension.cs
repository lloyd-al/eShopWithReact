using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Common.Infrastructure.Services;
using eShopWithReact.Common.Infrastructure.Utils.Swagger;
using eShopWithReact.Common.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace eShopWithReact.Common.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);

                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;

                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;

                // Supporting multiple versioning scheme
                config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-version"), new QueryStringApiVersionReader("api-version"));
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services, string apiName)
        {
            var apiVersionDescriptionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>(options =>
            {
                return new ConfigureSwaggerOptions(apiVersionDescriptionProvider, apiName);
            });

            services.AddSwaggerGen(
                options =>
                {
                    //options.OperationFilter<SwaggerDefaultValues>();
                    options.OperationFilter<RemoveVersionParameterFilter>();
                    options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

                    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    //options.IncludeXmlComments(xmlPath);
                });
        }

        public static void ConfigureMailSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSetting>(configuration.GetSection("MailSettings"));
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();


        public static void ConfigureMailService(this IServiceCollection services) =>
            services.AddTransient<IEmailService, EmailService>();

        public static void ConfigureUrlServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }
    }
}
