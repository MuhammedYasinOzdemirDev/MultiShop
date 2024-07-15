using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class RemoveOrderingCommandHandler:IRequestHandler<RemoveOrderingCommand,bool>
{
  
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _unitOfWork.Orderings.GetByIdAsync(request.Id);
        if (ordering == null)
        {
            throw new KeyNotFoundException("Ordering not found");
        }

        var value= await _unitOfWork.Orderings.DeleteAsync(ordering);
        _unitOfWork.CompleteAsync();
        return value;
    }
   
}