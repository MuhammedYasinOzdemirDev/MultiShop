using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

public class GetAllAddressQuery :  IRequest<List<GetAddressQueryResult>>
{
}