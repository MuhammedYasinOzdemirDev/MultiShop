using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderDetailHandlers;

public class UpdateOrderDetailCommandHandler:IRequestHandler<UpdateOrderDetailCommand,bool>
{
    private readonly IRepository<OrderDetail> _repository;

    public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
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
            return  await _repository.UpdateAsync(orderDetail);
    }
    
}