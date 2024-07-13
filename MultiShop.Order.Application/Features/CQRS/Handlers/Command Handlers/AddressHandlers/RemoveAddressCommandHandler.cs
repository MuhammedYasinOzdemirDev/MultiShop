using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.AddressHandlers;

public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommand>
{
    private readonly IRepository<Address> _repository;

    public RemoveAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id);
        if (address == null)
        {
            throw new KeyNotFoundException("Address not found");
        }

        await _repository.DeleteAsync(address);
    }
}