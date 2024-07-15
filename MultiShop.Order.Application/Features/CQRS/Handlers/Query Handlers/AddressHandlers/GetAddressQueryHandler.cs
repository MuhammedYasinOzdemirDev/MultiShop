using MediatR;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Query_Handlers.AddressHandlers;

public class GetAddressQueryHandler:IRequestHandler<GetAllAddressQuery, List<GetAddressQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAddressQueryResult>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
    {
        var addresses = await _unitOfWork.Addresses.GetAllAsync();

        return addresses.Select(address => new GetAddressQueryResult
        {
            AddressId = address.AddressId,
            UserId = address.UserId,
            District = address.District,
            City = address.City,
            Detail = address.Detail
        }).ToList();
    }
}