using MongoDB.Driver;
using eShopWithReact.Services.ProductCatalog.Core.Entities;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;

namespace eShopWithReact.Services.ProductCatalog.Infrastructure.DataContexts
{
    public class CatalogDbContext : ICatalogDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public CatalogDbContext(ICatalogDatabaseSetting settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);

            Categories = _db.GetCollection<Category>(settings.CategoryCollectionName);
            Products = _db.GetCollection<Product>(settings.ProductCollectionName);

            CatalogDbContextSeed.SeedData(Categories, Products);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public IMongoCollection<Category> Categories { get; }
        public IMongoCollection<Product> Products { get; }
    }
}
