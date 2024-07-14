using FluentValidation;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Services.Interfaces;

namespace MultiShop.Order.Application.Services;

public class AddressService:IAddressService
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateAddressCommand> _createValidator;
    private readonly IValidator<UpdateAddressCommand> _updateValidator;

    public AddressService(IMediator mediator, IValidator<CreateAddressCommand> createValidator, IValidator<UpdateAddressCommand> updateValidator)
    {
        _mediator = mediator;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<int> CreateAddressAsync(CreateAddressCommand command)
    {
        var validationResult = await _createValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> UpdateAddressAsync(UpdateAddressCommand command)
    {
        var validationResult = await _updateValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> RemoveAddressAsync(RemoveAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<GetAddressByIdQueryResult> GetAddressByIdAsync(GetAddressByIdQuery query)
    {
        return await _mediator.Send(query);
    }

    public async Task<List<GetAddressQueryResult>> GetAllAddressesAsync()
    {
        return (List<GetAddressQueryResult>)await _mediator.Send(new GetAddressQueryResult());
    }
}