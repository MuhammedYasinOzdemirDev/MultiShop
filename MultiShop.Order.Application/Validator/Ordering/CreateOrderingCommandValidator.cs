using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Handlers;

namespace MultiShop.Order.Application.Validator.Ordering;

public class CreateOrderingCommandValidator : AbstractValidator<CreateOrderingCommand>
{
    public CreateOrderingCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}