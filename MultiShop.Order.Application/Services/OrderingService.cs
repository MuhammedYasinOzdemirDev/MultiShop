using FluentValidation;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Handlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.CQRS.Results;
using MultiShop.Order.Application.Services.Interfaces;

namespace MultiShop.Order.Application.Services;

public class OrderingService : IOrderingService
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateOrderingCommand> _createValidator;
    private readonly IValidator<UpdateOrderingCommand> _updateValidator;

    public OrderingService(IMediator mediator, IValidator<CreateOrderingCommand> createValidator, IValidator<UpdateOrderingCommand> updateValidator)
    {
        _mediator = mediator;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<int> CreateOrderingAsync(CreateOrderingCommand command)
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
                throw new ApplicationException("Validation failed for CreateOrderingCommand", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while creating the ordering: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateOrderingAsync(UpdateOrderingCommand command)
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
                throw new ApplicationException("Validation failed for UpdateOrderingCommand", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the ordering: {ex.Message}", ex);
            }
        }

        public async Task<bool> RemoveOrderingAsync(RemoveOrderingCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while removing the ordering: {ex.Message}", ex);
            }
        }

        public async Task<GetOrderingByIdResult> GetOrderingByIdAsync(GetOrderingByIdQuery query)
        {
            try
            {
                return await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while fetching the ordering: {ex.Message}", ex);
            }
        }

        public async Task<List<GetOrderingQueryResult>> GetAllOrderingsAsync()
        {
            try
            {
                return await _mediator.Send(new GetAllOrderingQuery());
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while fetching the orderings: {ex.Message}", ex);
            }
        }
}