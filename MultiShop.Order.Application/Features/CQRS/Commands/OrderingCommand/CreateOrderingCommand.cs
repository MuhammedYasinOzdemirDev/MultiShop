using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Handlers;

public class CreateOrderingCommand:IRequest<int>
{
    public string UserId { get; set; }
}