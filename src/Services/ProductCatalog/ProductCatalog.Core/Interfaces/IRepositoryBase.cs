using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;


namespace eShopWithReact.Services.ProductCatalog.Core.Interfaces
{
    public interface IIdentity
    {
        ObjectId Id { get; set; }
    }
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
