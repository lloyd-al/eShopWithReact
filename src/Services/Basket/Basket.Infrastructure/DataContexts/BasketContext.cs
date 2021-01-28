using StackExchange.Redis;
using eShopWithReact.Services.Basket.Core.Interfaces;

namespace eShopWithReact.Services.Basket.Infrastructure.DataContexts
{
    public class BasketContext : IBasketContext
    {
        public ConnectionMultiplexer Connection { get; }

        public IDatabase Redis { get; }

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            Connection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }


    }
}
