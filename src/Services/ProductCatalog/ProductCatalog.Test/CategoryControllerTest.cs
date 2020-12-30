using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using eShopWithReact.Services.ProductCatalog.Api.Controllers.v1;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using eShopWithReact.Services.ProductCatalog.Core.Entities;
using FluentAssertions;

namespace eShopWithReact.Services.ProductCatalog.Test
{
    public class CategoryControllerTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        public HttpClient _client { get; }

        public CategoryControllerTest(WebApplicationFactory<Api.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Category()
        {
            var response = await _client.GetAsync("/category");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<Category[]>(await response.Content.ReadAsStringAsync());
            category.Should().HaveCount(5);
        }
    }
}
