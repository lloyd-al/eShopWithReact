using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace eShopWithReact.Common.Infrastructure.Utils.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider _provider;
        private readonly string _apiName;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, string apiName)
        {
            _provider = provider;
            _apiName = apiName;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _apiName,
                //Version = description.ApiVersion.ToString(),
                Version = description.GroupName.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " - DEPRECATED";
            }

            return info;
        }
    }
}
