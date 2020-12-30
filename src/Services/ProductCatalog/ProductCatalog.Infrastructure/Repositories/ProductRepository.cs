using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using eShopWithReact.Common.Core.Entities.Filters;
using eShopWithReact.Services.ProductCatalog.Core.Entities;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;


namespace eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogDbContext _context;

        public ProductRepository(ICatalogDbContext catalogContext)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<(IEnumerable<Product> products, long totalRecords, int totalPages)> GetAll(RequestParameters pagefilter)
        {
            var collection = _context.GetCollection<Product>(nameof(_context.Products));

            var results = await collection.AggregateByPage(
                Builders<Product>.Filter.Empty, 
                Builders<Product>.Sort.Ascending(x => x.Id), 
                page: pagefilter.PageNumber, 
                pageSize: pagefilter.PageSize);

            return results;
        }

        public async Task<IEnumerable<Product>> GetByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.ProductName, productName);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<(IEnumerable<Product> products, long totalRecords, int totalPages)> GetByCategory(string category, RequestParameters pagefilter)
        {
            //FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            var collection = _context.GetCollection<Product>(nameof(_context.Products));

            var results = await collection.AggregateByPage(
                Builders<Product>.Filter.Eq(p => p.Category, category),
                Builders<Product>.Sort.Ascending(x => x.Id),
                page: pagefilter.PageNumber,
                pageSize: pagefilter.PageSize);

            return results;
        }


        public async Task<Product> GetById(string id)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }


        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);

        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged 
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
