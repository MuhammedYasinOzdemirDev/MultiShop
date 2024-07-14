using System.Linq.Expressions;

namespace MultiShop.Order.Infrastructure.Persistence.Interfaces;

public interface IRepository<T> where T:class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entiy);
    Task<bool> DeleteAsync(T entity);
    Task<T> GetByIdFilterAsync(Expression<Func<T, bool>> filter);
}