using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;

public class GetAllOrderingQuery:IRequest<List<GetOrderingQueryResult>>
{
    
}