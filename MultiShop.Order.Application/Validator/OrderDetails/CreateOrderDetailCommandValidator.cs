using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;

namespace MultiShop.Order.Application.Validator.OrderDetails;

public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
{
    public CreateOrderDetailCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0.");
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required.")
            .MaximumLength(100).WithMessage("ProductName must be less than 100 characters.");
        RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("ProductPrice must be greater than 0.");
        RuleFor(x => x.ProductAmount).GreaterThan(0).WithMessage("ProductAmount must be greater than 0.");
        RuleFor(x => x.ProductTotalPrice).GreaterThan(0).WithMessage("ProductTotalPrice must be greater than 0.");
        RuleFor(x => x.OrderingId).GreaterThan(0).WithMessage("OrderingId must be greater than 0.");
    }
}