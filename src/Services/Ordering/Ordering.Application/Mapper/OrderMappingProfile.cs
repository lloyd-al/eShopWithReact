using AutoMapper;
using eShopWithReact.Services.Ordering.Application.Commands;
using eShopWithReact.Services.Ordering.Application.Responses;
using eShopWithReact.Services.Ordering.Core.Entities;

namespace eShopWithReact.Services.Ordering.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}
