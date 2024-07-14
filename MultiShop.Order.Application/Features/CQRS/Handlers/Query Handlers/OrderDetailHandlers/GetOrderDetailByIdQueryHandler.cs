using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.OrderDetailHandlers;

public class GetOrderDetailByIdQueryHandler:IRequestHandler<GetOrderDetailByIdQuery,GetOrderDetailByIdResult>
{
    private readonly IRepository<OrderDetail> _repository;

    public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task<GetOrderDetailByIdResult> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await _repository.GetByIdAsync(request.Id);
        return new GetOrderDetailByIdResult
        {
            OrderDetailId = orderDetails.OrderDetailId,
            ProductId = orderDetails.ProductId,
            ProductName = orderDetails.ProductName,
            ProductPrice = orderDetails.ProductPrice,
            ProductAmount = orderDetails.ProductAmount,
            ProductTotalPrice = orderDetails.ProductTotalPrice,
            OrderingId = orderDetails.OrderingId
        };
    }
}