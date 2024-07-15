using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;
using MultiShop.Order.Infrastructure.Persistence.Repositories;

namespace MultiShop.Order.Infrastructure.Registration;

public static class RepositoryRegistration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Address>,AddressRepository>();
        services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();
        services.AddScoped<IRepository<Ordering>, OrderingRepository>();
    }
}