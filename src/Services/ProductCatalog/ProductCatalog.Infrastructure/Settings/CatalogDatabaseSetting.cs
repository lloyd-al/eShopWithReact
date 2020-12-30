using eShopWithReact.Services.ProductCatalog.Core.Interfaces;


namespace eShopWithReact.Services.ProductCatalog.Infrastructure.Settings
{
    public class CatalogDatabaseSetting : ICatalogDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
    }
}
