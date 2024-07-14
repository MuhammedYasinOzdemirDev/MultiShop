using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

public class GetOrderDetailByIdQuery:IRequest<GetOrderDetailByIdResult>
{
    public int Id { get; set; }

    public GetOrderDetailByIdQuery(int id)
    {
        Id = id;
    }
}