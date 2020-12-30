using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
using MongoDB.Bson;
using MongoDB.Driver;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;

namespace eShopWithReact.Services.ProductCatalog.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    {
        private readonly ICatalogDbContext _context;
        protected IMongoCollection<T> _dbCollection;

        public RepositoryBase(ICatalogDbContext catalogContext)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _dbCollection = _context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<T>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var objectId = new ObjectId(id);

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", objectId);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task Create(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(T).Name + " object is null");
            }
            await _dbCollection.InsertOneAsync(obj);
        }

        public void Update(T obj)
        {
            _dbCollection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", obj.GetId()), obj);
        }

        public void Delete(string id)
        {
            var objectId = new ObjectId(id);
            _dbCollection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));

        }

    }
}
