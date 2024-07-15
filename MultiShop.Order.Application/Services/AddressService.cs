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
        try
        {
            var validationResult = await _createValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            // Validation exception handling
            throw new ApplicationException("Validation failed for CreateAddressCommand", ex);
        }
        catch (Exception ex)
        {
            // General exception handling
            throw new ApplicationException("An error occurred while creating the address", ex);
        }
    }

    public async Task<bool> UpdateAddressAsync(UpdateAddressCommand command)
    {
        try
        {
            var validationResult = await _updateValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            // Validation exception handling
            throw new ApplicationException("Validation failed for UpdateAddressCommand", ex);
        }
        catch (Exception ex)
        {
            // General exception handling
            throw new ApplicationException("An error occurred while updating the address", ex);
        }
    }

    public async Task<bool> RemoveAddressAsync(RemoveAddressCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            // General exception handling
            throw new ApplicationException("An error occurred while removing the address", ex);
        }
    }

    public async Task<GetAddressByIdQueryResult> GetAddressByIdAsync(GetAddressByIdQuery query)
    {
        try
        {
            return await _mediator.Send(query);
        }
        catch (Exception ex)
        {
            // General exception handling
            throw new ApplicationException("An error occurred while retrieving the address", ex);
        }
    }

    public async Task<List<GetAddressQueryResult>> GetAllAddressesAsync()
    {
        try
        {
            return await _mediator.Send(new GetAllAddressQuery());
        }
        catch (Exception ex)
        {
            // General exception handling
            throw new ApplicationException("An error occurred while retrieving all addresses", ex);
        }
    }
}