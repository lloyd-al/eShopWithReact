using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using eShopWithReact.Services.UserAuthentication.Core;
using eShopWithReact.Services.UserAuthentication.Api.Extensions;
using eShopWithReact.Services.UserAuthentication.Infrastructure;
using eShopWithReact.Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using UserAuthentication.Api.Middlewares;
using eShopWithReact.Services.UserAuthentication.Infrastructure.DataContexts;
using Microsoft.AspNetCore.Identity;
using eShopWithReact.Services.UserAuthentication.Core.Entities;

namespace eShopWithReact.Services.UserAuthentication.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.ConfigureDatabase(Configuration);

            services.ConfigureExtensions(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerExtension(provider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
