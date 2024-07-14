using MediatR;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class UpdateOrderingCommandHandler:IRequestHandler<UpdateOrderingCommand,bool>
{
    private readonly IRepository<Ordering> _repository;

    public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }
    

    public async Task<bool> Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _repository.GetByIdAsync(request.OrderingId);
        if (ordering == null)
        {
            throw new KeyNotFoundException("Ordering not found");
        }
        ordering.TotalPrice = request.TotalPrice;
        ordering.OrderDetails = request.OrderDetails;
        ordering.UserId = request.UserId;
        return await _repository.UpdateAsync(ordering);
    }
}