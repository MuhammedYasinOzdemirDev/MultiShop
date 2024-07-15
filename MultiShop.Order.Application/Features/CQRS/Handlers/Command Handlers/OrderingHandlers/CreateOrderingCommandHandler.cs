using MediatR;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class CreateOrderingCommandHandler:IRequestHandler<CreateOrderingCommand,int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = new Ordering
        {
            UserId = request.UserId,
            TotalPrice = 0,
            OrderDate = DateTime.Now,
            OrderDetails = new List<OrderDetail>()
        };
        var value= await _unitOfWork.Orderings.CreateAsync(ordering);
        _unitOfWork.CompleteAsync();
        return value;
    }
}