using System;
using System.Collections.Generic;
using System.Text;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Core.Entities;

namespace eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ICatalogDbContext _context;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(ICatalogDbContext catalogContext)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }
        public IProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }
    }
}
