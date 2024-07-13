using FluentValidation;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Validator;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.District).NotEmpty().MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Detail).NotEmpty().MaximumLength(250);
    }
}