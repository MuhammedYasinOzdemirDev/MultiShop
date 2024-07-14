using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class OrderDetailRepository:IRepository<OrderDetail>
{
    private readonly DbContext _context;
    private readonly DbSet<OrderDetail> _dbSet;

    public OrderDetailRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<OrderDetail>();
    }
    public async Task<List<OrderDetail>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<OrderDetail> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<int> CreateAsync(OrderDetail entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (int)_context.Entry(entity).Property("OrderDetailId").CurrentValue;
    }

    public async Task<bool> UpdateAsync(OrderDetail entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(OrderDetail entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<OrderDetail> GetByIdFilterAsync(Expression<Func<OrderDetail, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }
}