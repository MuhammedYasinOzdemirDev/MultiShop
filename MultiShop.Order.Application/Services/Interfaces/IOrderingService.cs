using MultiShop.Order.Application.Features.CQRS.Handlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.CQRS.Results;

namespace MultiShop.Order.Application.Services.Interfaces;

public interface IOrderingService
{
    Task<int> CreateOrderingAsync(CreateOrderingCommand command);
    Task<bool> UpdateOrderingAsync(UpdateOrderingCommand command);
    Task<bool> RemoveOrderingAsync(RemoveOrderingCommand command);
    Task<GetOrderingByIdResult> GetOrderingByIdAsync(GetOrderingByIdQuery query);
    Task<List<GetOrderingQueryResult>> GetAllOrderingsAsync();
}