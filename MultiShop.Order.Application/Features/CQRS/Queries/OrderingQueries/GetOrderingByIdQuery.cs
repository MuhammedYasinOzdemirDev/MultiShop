using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;

public class GetOrderingByIdQuery:IRequest<GetOrderingByIdResult>
{
    public int Id { get; set; }

    public GetOrderingByIdQuery(int id)
    {
        Id = id;
    }
}