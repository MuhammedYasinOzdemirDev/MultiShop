using System.Linq.Expressions;

namespace MultiShop.Order.Infrastructure.Persistence.Interfaces;

public interface IRepository<T> where T:class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entiy);
    Task<T> DeleteAsync(T entity);
    Task<T> GetByIdFilterAsync(Expression<Func<T, bool>> filter);
}