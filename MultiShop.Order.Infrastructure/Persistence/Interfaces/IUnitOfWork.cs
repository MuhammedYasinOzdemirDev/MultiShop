using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork:IDisposable
{
     IRepository<Address> Addresses { get; }
     IRepository<OrderDetail> OrderDetails { get; }
     IRepository<Ordering> Orderings { get; }
     Task<int> CompleteAsync();
}