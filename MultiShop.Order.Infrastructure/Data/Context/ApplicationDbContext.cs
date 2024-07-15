using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Configuration;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Infrastructure.Data.Context;

public class ApplicationDbContext:DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Ordering> Orderings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new OrderingConfiguration());
    }
}