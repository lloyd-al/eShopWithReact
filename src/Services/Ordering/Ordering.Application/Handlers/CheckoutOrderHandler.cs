using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eShopWithReact.Services.Ordering.Application.Commands;
using eShopWithReact.Services.Ordering.Application.Mapper;
using eShopWithReact.Services.Ordering.Application.Responses;
using eShopWithReact.Services.Ordering.Core.Entities;
using eShopWithReact.Services.Ordering.Core.Interfaces;



namespace eShopWithReact.Services.Ordering.Application.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Order>(request);
            if (orderEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newOrder = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(newOrder);
            return orderResponse;
        }
    }
}
