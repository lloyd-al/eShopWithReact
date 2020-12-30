using System.Collections.Generic;
using System.Threading.Tasks;
using eShopWithReact.Services.ProductCatalog.Core.Entities;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetByName(string categoryName);
        Task<Category> GetById(string id);
        Task Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(string id);
    }
}
