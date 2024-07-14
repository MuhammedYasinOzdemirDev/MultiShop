using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Handlers;

public class RemoveOrderingCommand:IRequest<bool>
{
    public int Id { get; set; }

    public RemoveOrderingCommand(int id)
    {
        Id = id;
    }
}