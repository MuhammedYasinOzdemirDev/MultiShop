using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;

namespace MultiShop.Order.Application.Services.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(CreateAddressCommand command);
    Task<bool> UpdateAddressAsync(UpdateAddressCommand command);
    Task<bool> RemoveAddressAsync(RemoveAddressCommand command);
    Task<GetAddressByIdQueryResult> GetAddressByIdAsync(GetAddressByIdQuery query);
    Task<List<GetAddressQueryResult>> GetAllAddressesAsync();
}