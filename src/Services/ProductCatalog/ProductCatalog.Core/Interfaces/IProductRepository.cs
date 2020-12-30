using System.Collections.Generic;
using System.Threading.Tasks;
using eShopWithReact.Common.Core.Entities.Filters;
using eShopWithReact.Services.ProductCatalog.Core.Entities;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> products, long totalRecords, int totalPages)> GetAll(RequestParameters filter);
        Task<(IEnumerable<Product> products, long totalRecords, int totalPages)> GetByCategory(string category, RequestParameters filter);
        Task<IEnumerable<Product>> GetByName(string productName);
        Task<Product> GetById(string id);
        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}
