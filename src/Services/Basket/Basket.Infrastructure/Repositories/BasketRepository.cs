using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using eShopWithReact.Services.Basket.Core.Interfaces;
using eShopWithReact.Services.Basket.Core.Entities;
using StackExchange.Redis;

namespace eShopWithReact.Services.Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CustomerBasket> GetBasketAsync(string buyerId)
        {
            var basket = await _context
                                .Redis
                                .StringGetAsync(buyerId);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var updated = await _context
                              .Redis
                              .StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!updated)
            {
                return null;
            }
            return await GetBasketAsync(basket.BuyerId);
        }

        public async Task<bool> DeleteBasketAsync(string buyerId)
        {
            return await _context
                            .Redis
                            .KeyDeleteAsync(buyerId);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            var endpoint = _context.Connection.GetEndPoints();
            return _context.Connection.GetServer(endpoint.First());
        }
    }
}
