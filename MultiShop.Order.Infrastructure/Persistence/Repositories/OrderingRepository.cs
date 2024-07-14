using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class OrderingExperssionepository:IRepository<Ordering>
{
    private readonly DbContext _context;
    private readonly DbSet<Ordering> _dbSet;

    public OrderingExperssionepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<Ordering>();
    }
    public async Task<List<Ordering>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Ordering> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<int> CreateAsync(Ordering entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (int)_context.Entry(entity).Property("OrderDetailId").CurrentValue;
    }

    public async Task<bool> UpdateAsync(Ordering entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Ordering entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Ordering> GetByIdFilterAsync(Expression<Func<Ordering, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }
}