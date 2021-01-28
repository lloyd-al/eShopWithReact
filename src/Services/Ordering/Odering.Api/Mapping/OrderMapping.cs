using AutoMapper;
using eShopWithReact.Common.EventBusRabbitMQ.Events;
using eShopWithReact.Services.Ordering.Application.Commands;

namespace eShop.Ordering.Api.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
