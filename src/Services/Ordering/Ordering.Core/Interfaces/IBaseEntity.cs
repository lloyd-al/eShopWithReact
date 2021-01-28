using System;

namespace eShopWithReact.Services.Ordering.Core.Interfaces
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; }
    }
}
