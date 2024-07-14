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
        var validationResult = await _createValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> UpdateOrderingAsync(UpdateOrderingCommand command)
    {
        var validationResult = await _updateValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await _mediator.Send(command);
    }

    public async Task<bool> RemoveOrderingAsync(RemoveOrderingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<GetOrderingByIdResult> GetOrderingByIdAsync(GetOrderingByIdQuery query)
    {
        return await _mediator.Send(query);
    }

    public async Task<List<GetOrderingQueryResult>> GetAllOrderingsAsync()
    {
        return (List<GetOrderingQueryResult>)await _mediator.Send(new GetOrderingQueryResult());
    }
}