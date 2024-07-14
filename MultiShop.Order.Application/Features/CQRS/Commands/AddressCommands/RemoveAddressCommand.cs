using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;

public class RemoveAddressCommand:IRequest<bool>
{
    public int Id { get; set; }

    public RemoveAddressCommand(int id)
    {
        Id = id;
    }
}