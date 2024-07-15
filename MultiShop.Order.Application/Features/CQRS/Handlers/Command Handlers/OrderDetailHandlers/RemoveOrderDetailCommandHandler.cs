using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderDetailHandlers;

public class RemoveOrderDetailCommandHandler:IRequestHandler<RemoveOrderDetailCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = await _unitOfWork.OrderDetails.GetByIdAsync(request.Id);
        if (orderDetail == null)
        {
            throw new KeyNotFoundException("orderDetail not found");
        }

        var value= await _unitOfWork.OrderDetails.DeleteAsync(orderDetail);
        await _unitOfWork.CompleteAsync();
        return value;
    }
}