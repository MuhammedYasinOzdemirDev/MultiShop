using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Infrastructure.Persistence;
using MultiShop.Order.Infrastructure.Persistence.Interfaces;

namespace MultiShop.Order.Infrastructure.Registration;

public static class UnitOfWorkRegistration
{
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}