using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

public class GetAddressByIdQuery: IRequest<GetAddressByIdQueryResult>
{
    public int Id { get; set; }

    public GetAddressByIdQuery(int id)
    {
        Id = id;
    }
}