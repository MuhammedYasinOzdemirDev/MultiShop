using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.CQRS.Results;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.OrderingHandlers;

public class GetOrderingQueryHandler:IRequestHandler<GetAllOrderingQuery,List<GetOrderingQueryResult>>
{
    private readonly IRepository<Ordering> _repository;

    public GetOrderingQueryHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }
    public async Task<List<GetOrderingQueryResult>> Handle(GetAllOrderingQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync();
        return values.Select(value => new GetOrderingQueryResult
        {
            OrderingId = value.OrderingId,
            OrderDate = value.OrderDate,
            UserId = value.UserId,
            TotalPrice = value.TotalPrice,
        }).ToList();
    }
}