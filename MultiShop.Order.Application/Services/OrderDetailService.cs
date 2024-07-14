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
        var validationResult = await _createValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> UpdateOrderDetailAsync(UpdateOrderDetailCommand command)
    {
        var validationResult = await _updateValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> RemoveOrderDetailAsync(RemoveOrderDetailCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<GetOrderDetailByIdResult> GetOrderDetailByIdAsync(GetOrderDetailByIdQuery query)
    {
        return await _mediator.Send(query);
    }

    public async Task<List<GetOrderDetailQueryResult>> GetAllOrderDetailsAsync()
    {
        return await _mediator.Send(new GetAllOrderDetailQuery());
    }
}