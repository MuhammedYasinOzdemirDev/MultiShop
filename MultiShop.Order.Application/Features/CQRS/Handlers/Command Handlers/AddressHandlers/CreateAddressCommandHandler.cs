using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.AddressHandlers;

public class CreateAddressCommandHandler:IRequestHandler<CreateAddressCommand, int>
{
    private readonly IRepository<Address> _repository;

    public CreateAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = new Address
        {
            City = request.City,
            Detail = request.Detail,
            District = request.District,
            UserId = request.UserId
        };

        var createdAddressId = await _repository.CreateAsync(address);
        return createdAddressId;
    }
}