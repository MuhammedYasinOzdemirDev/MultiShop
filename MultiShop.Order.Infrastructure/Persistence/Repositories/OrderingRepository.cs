using System.Linq.Expressions;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class OrderingRExperssionepository:IRepository<Ordering>
{
    public Task<List<Ordering>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ordering> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Ordering> CreateAsync(Ordering entity)
    {
        throw new NotImplementedException();
    }

    public Task<Ordering> UpdateAsync(Ordering entiy)
    {
        throw new NotImplementedException();
    }

    public Task<Ordering> DeleteAsync(Ordering entity)
    {
        throw new NotImplementedException();
    }

    public Task<Ordering> GetByIdFilterAsync(Expression<Func<Ordering, bool>> filter)
    {
        throw new NotImplementedException();
    }
}