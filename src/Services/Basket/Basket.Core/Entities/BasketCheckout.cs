using System.Collections.Generic;

namespace eShopWithReact.Services.Basket.Core.Entities
{
    public class BasketCheckout
    {
        public string Buyer { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public int PaymentMethod { get; set; }
    }
}
