using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderDetailHandlers;

public class RemoveOrderDetailCommandHandler:IRequestHandler<RemoveOrderDetailCommand,bool>
{
    private readonly IRepository<OrderDetail> _repository;

    public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(RemoveOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = await _repository.GetByIdAsync(request.Id);
        if (orderDetail == null)
        {
            throw new KeyNotFoundException("orderDetail not found");
        }

        return await _repository.DeleteAsync(orderDetail);
    }
}