using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopWithReact.Services.Ordering.Core.Entities;
using eShopWithReact.Services.Ordering.Core.Interfaces;
using eShopWithReact.Services.Ordering.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;


namespace eShopWithReact.Services.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.Buyer == userName)
                      .ToListAsync();

            return orderList;
        }
    }
}
