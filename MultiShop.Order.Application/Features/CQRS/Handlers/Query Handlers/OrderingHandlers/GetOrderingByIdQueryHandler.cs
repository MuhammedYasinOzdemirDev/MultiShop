using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.CQRS.Results;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.OrderingHandlers;

public class GetOrderingByIdQueryHandler:IRequestHandler<GetOrderingByIdQuery,GetOrderingByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderingByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetOrderingByIdResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _unitOfWork.Orderings.GetByIdAsync(request.Id);
        return new GetOrderingByIdResult
        {
            OrderingId = value.OrderingId,
            OrderDate = value.OrderDate,
            UserId = value.UserId,
            TotalPrice = value.TotalPrice,
            OrderDetails = value.OrderDetails.ToList()
        };
    }
}