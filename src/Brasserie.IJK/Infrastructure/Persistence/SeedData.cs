using Brasserie.IJK.Domain.Customers;
using Brasserie.IJK.Domain.Orders;
using Brasserie.IJK.Domain.Products;
using Brasserie.IJK.Domain.Shared;

namespace Brasserie.IJK.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<BrasserieDbContext>();

            if (dbContext.Customers.Any())
                return;

            var burger = new Product
            {
                Name = "Burger",
                Price = 15.50m,
                QuantityInStock = 500
            };

            var fries = new Product
            {
                Name = "Fries",
                Price = 4.50m,
                QuantityInStock = 500
            };

            var cola = new Product
            {
                Name = "Cola",
                Price = 3.00m,
                QuantityInStock = 500
            };

            dbContext.Products.AddRange(burger, fries, cola);

            var customer = Customer.Create(
                new PersonalInfo
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateOnly(1990,1,1),
                    Gender = Gender.Male
                },
                new ContactInfo
                {
                    EmailAddress = "john.doe@email.com",
                    PhoneNumber = "0123456789"                    
                },
                new AddressInfo
                {
                    Street = "Main Street",
                    HouseNumber = "12",
                    PostalCode = "1234 AB",
                    City = "Eindhoven",
                    Country = "NL"
                }
            );

            var order = new Order
            {
                Customer = customer,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
            };

            order.AddOrderLine(burger, 2);
            order.AddOrderLine(fries, 1);
            order.AddOrderLine(cola, 2);

            dbContext.Customers.Add(customer);
            dbContext.Orders.Add(order);

            dbContext.SaveChanges();
        }
    }
}
