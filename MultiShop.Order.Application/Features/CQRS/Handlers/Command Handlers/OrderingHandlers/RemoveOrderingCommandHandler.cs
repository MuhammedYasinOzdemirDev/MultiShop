using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class RemoveOrderingCommandHandler:IRequestHandler<RemoveOrderingCommand,bool>
{
  
    private readonly IRepository<Address> _repository;

    public RemoveOrderingCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _repository.GetByIdAsync(request.Id);
        if (ordering == null)
        {
            throw new KeyNotFoundException("Ordering not found");
        }

        return await _repository.DeleteAsync(ordering);
    }
   
}