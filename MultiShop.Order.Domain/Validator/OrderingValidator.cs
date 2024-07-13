using FluentValidation;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Validator;

public class OrderingValidator : AbstractValidator<Ordering>
{
    public OrderingValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TotalPrice).GreaterThan(0);
        RuleFor(x => x.OrderDate).NotEmpty();
    }
}