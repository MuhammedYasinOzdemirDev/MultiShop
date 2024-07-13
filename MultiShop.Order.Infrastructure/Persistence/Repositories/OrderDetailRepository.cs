using System.Linq.Expressions;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class OrderDetailRepository:IRepository<OrderDetail>
{
    public Task<List<OrderDetail>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> CreateAsync(OrderDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> UpdateAsync(OrderDetail entiy)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> DeleteAsync(OrderDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> GetByIdFilterAsync(Expression<Func<OrderDetail, bool>> filter)
    {
        throw new NotImplementedException();
    }
}