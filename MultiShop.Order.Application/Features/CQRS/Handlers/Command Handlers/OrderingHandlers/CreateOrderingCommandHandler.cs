using MediatR;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.Command_Handlers.OrderingHandlers;

public class CreateOrderingCommandHandler:IRequestHandler<CreateOrderingCommand,int>
{
    private readonly IRepository<Ordering> _repository;

    public CreateOrderingCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
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
        return await _repository.CreateAsync(ordering);
    }
}