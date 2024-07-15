using FluentValidation;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Services.Interfaces;

namespace MultiShop.Order.Application.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateOrderDetailCommand> _createValidator;
    private readonly IValidator<UpdateOrderDetailCommand> _updateValidator;

    public OrderDetailService(IMediator mediator, IValidator<CreateOrderDetailCommand> createValidator, IValidator<UpdateOrderDetailCommand> updateValidator)
    {
        _mediator = mediator;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

     public async Task<int> CreateOrderDetailAsync(CreateOrderDetailCommand command)
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
                throw new ApplicationException("Validation failed for CreateOrderDetailCommand", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while creating the order detail: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateOrderDetailAsync(UpdateOrderDetailCommand command)
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
                throw new ApplicationException("Validation failed for UpdateOrderDetailCommand", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the order detail: {ex.Message}", ex);
            }
        }

        public async Task<bool> RemoveOrderDetailAsync(RemoveOrderDetailCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while removing the order detail: {ex.Message}", ex);
            }
        }

        public async Task<GetOrderDetailByIdResult> GetOrderDetailByIdAsync(GetOrderDetailByIdQuery query)
        {
            try
            {
                return await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while fetching the order detail: {ex.Message}", ex);
            }
        }

        public async Task<List<GetOrderDetailQueryResult>> GetAllOrderDetailsAsync()
        {
            try
            {
                return await _mediator.Send(new GetAllOrderDetailQuery());
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while fetching the order details: {ex.Message}", ex);
            }
        }
    }