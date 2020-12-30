using System;
using eShopWithReact.Common.Core.Entities.Filters;


namespace eShopWithReact.Common.Core.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(RequestParameters filter, string route);
    }
}
