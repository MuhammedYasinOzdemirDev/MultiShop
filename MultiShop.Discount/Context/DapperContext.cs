using System.Data;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using Npgsql;

namespace MultiShop.Discount.Context;

public class DapperContext:DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public DbSet<Coupon> Coupons { get; set; }
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}