using System;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface ICatalogDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
