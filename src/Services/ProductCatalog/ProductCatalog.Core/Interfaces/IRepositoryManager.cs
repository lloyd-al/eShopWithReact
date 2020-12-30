using System;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface IRepositoryManager
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
    }
}
