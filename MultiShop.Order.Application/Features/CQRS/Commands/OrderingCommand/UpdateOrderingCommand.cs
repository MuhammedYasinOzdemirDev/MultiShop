using MediatR;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers;

public class UpdateOrderingCommand:IRequest<bool>
{
    public int OrderingId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}