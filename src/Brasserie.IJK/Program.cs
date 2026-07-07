using Brasserie.IJK.Api.Endpoints.Customer;
using Brasserie.IJK.Api.Endpoints.Order;
using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Application.Services;
using Brasserie.IJK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BrasserieDbContext>(options =>
{
    options.UseInMemoryDatabase("BrasserieDb");
});

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Console.WriteLine(app.Environment.EnvironmentName);

SeedData.Initialize(app.Services);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCustomerEndpoints();
app.MapOrderEndpoints();

app.Run();
