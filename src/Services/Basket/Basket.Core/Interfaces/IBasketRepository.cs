using System.Collections.Generic;
using System.Threading.Tasks;
using eShopWithReact.Services.Basket.Core.Entities;

namespace eShopWithReact.Services.Basket.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string buyerId);
        //IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string buyerId);
    }
}
