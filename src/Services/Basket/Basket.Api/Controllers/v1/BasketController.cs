using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eShopWithReact.Common.EventBusRabbitMQ.Common;
using eShopWithReact.Common.EventBusRabbitMQ.Events;
using eShopWithReact.Common.EventBusRabbitMQ.Producer;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Services.Basket.Core.Interfaces;
using eShopWithReact.Services.Basket.Core.Entities;
using AutoMapper;


namespace eShopWithReact.Services.Basket.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BasketController : RootController
    {
        private readonly IBasketRepository _repository;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, EventBusRabbitMQProducer eventBus, IMapper mapper, ILoggerManager logger) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string buyerId)
        {
            var basket = await _repository.GetBasketAsync(buyerId);
            return Ok(basket ?? new CustomerBasket(buyerId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromBody] CustomerBasket basket)
        {
            return Ok(await _repository.UpdateBasketAsync(basket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string buyerId)
        {
            return Ok(await _repository.DeleteBasketAsync(buyerId));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get total price of the basket
            // remove the basket 
            // send checkout event to rabbitMq 

            var basket = await _repository.GetBasketAsync(basketCheckout.Buyer);
            if (basket == null)
            {
                _logger.LogError("Basket not exist with this user : " + basketCheckout.Buyer);
                return BadRequest();
            }

            var basketRemoved = await _repository.DeleteBasketAsync(basketCheckout.Buyer);
            if (!basketRemoved)
            {
                _logger.LogError("Basket can not deleted");
                return BadRequest();
            }

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {
                _logger.LogError("ERROR Publishing integration event: {eventMessage.RequestId} from {Basket}");
                throw;
            }

            return Accepted();
        }
    }
}
