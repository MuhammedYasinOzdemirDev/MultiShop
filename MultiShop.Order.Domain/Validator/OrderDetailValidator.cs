using FluentValidation;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Validator;

public class OrderDetailValidator : AbstractValidator<OrderDetail>
{
    public OrderDetailValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ProductPrice).GreaterThan(0);
        RuleFor(x => x.ProductAmount).GreaterThan(0);
        RuleFor(x => x.ProductTotalPrice).GreaterThan(0);
        RuleFor(x => x.OrderingId).GreaterThan(0);
    }
}