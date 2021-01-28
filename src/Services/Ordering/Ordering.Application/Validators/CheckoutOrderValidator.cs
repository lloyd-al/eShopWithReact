using eShopWithReact.Services.Ordering.Application.Commands;
using FluentValidation;

namespace eShopWithReact.Services.Ordering.Application.Validators
{
    public class CheckoutOrderValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderValidator()
        {
            RuleFor(x => x.Buyer)
                .NotEmpty();
        }
    }
}
