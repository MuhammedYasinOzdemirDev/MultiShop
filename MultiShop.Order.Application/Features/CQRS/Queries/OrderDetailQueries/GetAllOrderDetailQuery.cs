using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

public class GetAllOrderDetailQuery:IRequest<GetOrderDetailQueryResult>
{
    
}