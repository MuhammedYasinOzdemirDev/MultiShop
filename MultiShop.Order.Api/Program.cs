using FluentValidation;
using FluentValidation.AspNetCore;
using MultiShop.Order.Application.Handlers;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Domain.Validator;
using MultiShop.Order.Infrastructure.Registration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IValidator<Address>, AddressValidator>();
builder.Services.AddTransient<IValidator<OrderDetail>, OrderDetailValidator>();
builder.Services.AddTransient<IValidator<Ordering>, OrderingValidator>();
// Database context
builder.Services.AddMySqlDbContext(builder.Configuration);

// Unit of Work and Repositories
builder.Services.AddUnitOfWork();
builder.Services.AddRepositories();

builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddHealthChecks();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

