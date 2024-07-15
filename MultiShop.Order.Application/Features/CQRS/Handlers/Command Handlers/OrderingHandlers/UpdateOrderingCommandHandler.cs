using MediatR;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class UpdateOrderingCommandHandler:IRequestHandler<UpdateOrderingCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _unitOfWork.Orderings.GetByIdAsync(request.OrderingId);
        if (ordering == null)
        {
            throw new KeyNotFoundException("Ordering not found");
        }
        ordering.TotalPrice = request.TotalPrice;
        ordering.OrderDetails = request.OrderDetails;
        ordering.UserId = request.UserId;
        var value= await _unitOfWork.Orderings.UpdateAsync(ordering);
        await _unitOfWork.CompleteAsync();
        return value;
    }
}