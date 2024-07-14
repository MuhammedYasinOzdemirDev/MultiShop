using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;

namespace MultiShop.Order.Application.Validator.Address;


public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
        RuleFor(x => x.District).NotEmpty().WithMessage("District is required.")
            .MaximumLength(100).WithMessage("District must be less than 100 characters.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must be less than 100 characters.");
        RuleFor(x => x.Detail).NotEmpty().WithMessage("Detail is required.")
            .MaximumLength(250).WithMessage("Detail must be less than 250 characters.");
    }
}