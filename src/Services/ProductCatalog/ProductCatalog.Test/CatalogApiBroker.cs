using System.Net.Http;
using eShopWithReact.Services.ProductCatalog.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;

namespace eShopWithReact.Services.ProductCatalog.Test
{
    public partial class CatalogApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public CatalogApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.baseClient = this.webApplicationFactory.CreateClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.baseClient);
        }
    }
}
