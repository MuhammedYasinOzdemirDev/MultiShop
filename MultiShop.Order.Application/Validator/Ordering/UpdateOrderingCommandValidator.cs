using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Handlers;
using MultiShop.Order.Domain.Validator;

namespace MultiShop.Order.Application.Validator.Ordering;

public class UpdateOrderingCommandValidator : AbstractValidator<UpdateOrderingCommand>
{
    public UpdateOrderingCommandValidator()
    {
        RuleFor(x => x.OrderingId).GreaterThan(0).WithMessage("OrderingId must be greater than 0.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0).WithMessage("TotalPrice must be greater than or equal to 0.");
        RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailValidator());
    }
}