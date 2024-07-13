using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.AddressHandlers;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, GetAddressByIdQueryResult>
{
    private readonly IRepository<Address> _repository;

    public GetAddressByIdQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id);
        if (address == null)
        {
            throw new KeyNotFoundException("Address not found");
        }

        return new GetAddressByIdQueryResult
        {
            AddressId = address.AddressId,
            UserId = address.UserId,
            District = address.District,
            City = address.City,
            Detail = address.Detail
        };
    }
}