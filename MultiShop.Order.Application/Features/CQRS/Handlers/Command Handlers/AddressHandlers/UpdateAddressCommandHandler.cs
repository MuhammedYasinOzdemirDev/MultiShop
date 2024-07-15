using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.AddressHandlers;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.Addresses.GetByIdAsync(request.AddressId);
        if (address == null)
        {
            throw new KeyNotFoundException("Address not found");
        }

        address.UserId = request.UserId;
        address.District = request.District;
        address.City = request.City;
        address.Detail = request.Detail;

     var value=await _unitOfWork.Addresses.UpdateAsync(address);
     _unitOfWork.CompleteAsync();
     return value;
    }
}