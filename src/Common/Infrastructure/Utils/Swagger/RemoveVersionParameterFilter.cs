using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace eShopWithReact.Common.Infrastructure.Utils.Swagger
{
    /// <summary>
    /// Remove the API Version as a parameter from the Swagger document.
    /// </summary>
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "api-version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
