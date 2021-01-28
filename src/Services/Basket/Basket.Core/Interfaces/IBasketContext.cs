using StackExchange.Redis;

namespace eShopWithReact.Services.Basket.Core.Interfaces
{
    public interface IBasketContext
    {
        public ConnectionMultiplexer Connection { get; }
        IDatabase Redis { get; }
    }
}
