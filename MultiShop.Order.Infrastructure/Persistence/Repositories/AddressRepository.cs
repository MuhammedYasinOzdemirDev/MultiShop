using System.Linq.Expressions;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class AddressRepository:IRepository<Address>
{
    public Task<List<Address>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Address entiy)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetByIdFilterAsync(Expression<Func<Address, bool>> filter)
    {
        throw new NotImplementedException();
    }
}