using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderDetailHandlers;

public class UpdateOrderDetailCommandHandler:IRequestHandler<UpdateOrderDetailCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
       
            var orderDetail = new OrderDetail
            {
                OrderDetailId = request.OrderDetailId,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProductAmount = request.ProductAmount,
                ProductTotalPrice = request.ProductTotalPrice,
                OrderingId = request.OrderingId
            };
            var value=  await _unitOfWork.OrderDetails.UpdateAsync(orderDetail);
            _unitOfWork.CompleteAsync();
            return value;
    }
    
}