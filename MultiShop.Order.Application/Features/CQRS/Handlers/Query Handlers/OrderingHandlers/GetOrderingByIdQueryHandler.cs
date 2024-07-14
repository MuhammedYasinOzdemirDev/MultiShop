using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.CQRS.Results;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.OrderingHandlers;

public class GetOrderingByIdQueryHandler:IRequestHandler<GetOrderingByIdQuery,GetOrderingByIdResult>
{
    private readonly IRepository<Ordering> _repository;

    public GetOrderingByIdQueryHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task<GetOrderingByIdResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
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