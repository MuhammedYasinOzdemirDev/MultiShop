using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderDetailHandlers;

public class CreateOrderDetailCommandHandler:IRequestHandler<CreateOrderDetailCommand,int>
{
    private readonly IRepository<OrderDetail> _repository;

    public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task<int> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = new OrderDetail
        {
ProductId = request.ProductId,
ProductName = request.ProductName,
ProductPrice = request.ProductPrice,
ProductAmount = request.ProductAmount,
ProductTotalPrice = request.ProductTotalPrice,
OrderingId = request.OrderingId
        };
        var id = await _repository.CreateAsync(orderDetail);
        return id;
    }
}