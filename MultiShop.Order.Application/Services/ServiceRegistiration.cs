using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers;
using MultiShop.Order.Application.Services.Interfaces;
using MultiShop.Order.Application.Validator.Address;
using MultiShop.Order.Application.Validator.OrderDetails;
using MultiShop.Order.Application.Validator.Ordering;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Domain.Validator;

namespace MultiShop.Order.Application.Services;

public static class ServiceRegistiration
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
        
        // FluentValidation validators
        services.AddTransient<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
        services.AddTransient<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();
        services.AddTransient<IValidator<CreateOrderDetailCommand>, CreateOrderDetailCommandValidator>();
        services.AddTransient<IValidator<UpdateOrderDetailCommand>, UpdateOrderDetailCommandValidator>();
        services.AddTransient<IValidator<CreateOrderingCommand>, CreateOrderingCommandValidator>();
        services.AddTransient<IValidator<UpdateOrderingCommand>, UpdateOrderingCommandValidator>();
        
        // Services
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IOrderDetailService, OrderDetailService>();
        services.AddScoped<IOrderingService, OrderingService>();
    }
}