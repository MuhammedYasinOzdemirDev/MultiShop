using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.AddressHandlers;

public class CreateAddressCommandHandler:IRequestHandler<CreateAddressCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

        var createdAddressId = await _unitOfWork.Addresses.CreateAsync(address);
        await _unitOfWork.CompleteAsync();
        return createdAddressId;
    }
}