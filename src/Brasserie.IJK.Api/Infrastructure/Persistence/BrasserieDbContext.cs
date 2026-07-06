using Brasserie.IJK.Api.Domain.Customers;
using Brasserie.IJK.Api.Domain.Orders;
using Brasserie.IJK.Api.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.IJK.Api.Infrastructure.Persistence
{
    public class BrasserieDbContext(DbContextOptions<BrasserieDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Product> Products => Set<Product>();
    }
}
