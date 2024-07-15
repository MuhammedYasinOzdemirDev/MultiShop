using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.AddressHandlers;

public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.Addresses.GetByIdAsync(request.Id);
        if (address == null)
        {
            throw new KeyNotFoundException("Address not found");
        }

       var value= await _unitOfWork.Addresses.DeleteAsync(address);
       await _unitOfWork.CompleteAsync();
       return value;
    }
}