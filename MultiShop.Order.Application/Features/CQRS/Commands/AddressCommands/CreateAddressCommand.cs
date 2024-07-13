using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;

public class CreateAddressCommand:IRequest<int>
{
    public int UserId { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Detail { get; set; }
}