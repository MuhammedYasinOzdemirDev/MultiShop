using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.OrderDetailHandlers;

public class GetOrderDetailQueryHandler:IRequestHandler<GetAllOrderDetailQuery,List<GetOrderDetailQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderDetailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<GetOrderDetailQueryResult>> Handle(GetAllOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
        return orderDetails.Select(detail => new GetOrderDetailQueryResult {
                OrderDetailId = detail.OrderDetailId,
                ProductId = detail.ProductId,
                ProductName = detail.ProductName,
                ProductPrice = detail.ProductPrice,
                ProductAmount = detail.ProductAmount,
                ProductTotalPrice = detail.ProductTotalPrice,
                OrderingId = detail.OrderingId
            }).ToList();
    }
}