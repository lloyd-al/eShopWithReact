using eShopWithReact.Services.Ordering.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopWithReact.Services.Ordering.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string buyer);
    }
}
