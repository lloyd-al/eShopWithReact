using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Common.Infrastructure.DataContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopWithReact.Common.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _dbContext { get; set; }

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() : _dbContext.Set<T>().Where(expression);

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
    }
}
