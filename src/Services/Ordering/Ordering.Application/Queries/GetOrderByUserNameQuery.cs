using System;
using System.Collections.Generic;
using MediatR;
using eShopWithReact.Services.Ordering.Application.Responses;


namespace eShopWithReact.Services.Ordering.Application.Queries
{
    public class GetOrderByUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
