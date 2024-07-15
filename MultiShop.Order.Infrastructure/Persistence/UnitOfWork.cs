using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Data.Context;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;
using MultiShop.Order.Infrastructure.Persistence.Repositories;

namespace MultiShop.Order.Infrastructure.Persistence;

public class UnitOfWork:IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IRepository<Address> _addresses;
    private IRepository<OrderDetail> _orderDetails;
    private IRepository<Ordering> _orderings;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public IRepository<Address> Addresses => _addresses ??= new AddressRepository(_context);
    public IRepository<OrderDetail> OrderDetails =>_orderDetails ??= new OrderDetailRepository(_context);
    public IRepository<Ordering> Orderings  => _orderings ??= new OrderingRepository(_context);
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}