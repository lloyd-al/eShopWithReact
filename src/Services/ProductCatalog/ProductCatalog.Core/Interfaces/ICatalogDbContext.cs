using MongoDB.Driver;
using eShopWithReact.Services.ProductCatalog.Core.Entities;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface ICatalogDbContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Category> Categories { get; }

        IMongoCollection<T> GetCollection<T>(string name);
    }
}
