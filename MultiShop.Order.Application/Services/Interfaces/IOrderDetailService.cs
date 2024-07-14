using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;

namespace MultiShop.Order.Application.Services.Interfaces;

public interface IOrderDetailService
{
    Task<int> CreateOrderDetailAsync(CreateOrderDetailCommand command);
    Task<bool> UpdateOrderDetailAsync(UpdateOrderDetailCommand command);
    Task<bool> RemoveOrderDetailAsync(RemoveOrderDetailCommand command);
    Task<GetOrderDetailByIdResult> GetOrderDetailByIdAsync(GetOrderDetailByIdQuery query);
    Task<List<GetOrderDetailQueryResult>> GetAllOrderDetailsAsync();
}