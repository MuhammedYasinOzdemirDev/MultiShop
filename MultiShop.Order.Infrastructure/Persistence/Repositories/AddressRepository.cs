using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Persistence.Repositories;

public class AddressRepository:IRepository<Address>
{
    private readonly DbContext _context;
    private readonly DbSet<Address> _dbSet;

    public AddressRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<Address>();
    }
    public async Task<List<Address>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Address> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<int> CreateAsync(Address entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (int)_context.Entry(entity).Property("AddressId").CurrentValue;
    }

    public async Task<bool> UpdateAsync(Address entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Address entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Address> GetByIdFilterAsync(Expression<Func<Address, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }
}