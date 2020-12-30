using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Core.Entities;

namespace eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICatalogDbContext _context;

        public CategoryRepository(ICatalogDbContext catalogContext)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context
                            .Categories
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByName(string categoryName)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.ElemMatch(p => p.CategoryName, categoryName);

            return await _context
                          .Categories
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<Category> GetById(string id)
        {
            return await _context
                            .Categories
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }


        public async Task Create(Category category)
        {
            await _context.Categories.InsertOneAsync(category);

        }

        public async Task<bool> Update(Category category)
        {
            var updateResult = await _context
                                        .Categories
                                        .ReplaceOneAsync(filter: g => g.Id == category.Id, replacement: category);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Categories
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
