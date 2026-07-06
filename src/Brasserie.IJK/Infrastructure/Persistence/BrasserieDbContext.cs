using Brasserie.IJK.Domain.Customers;
using Brasserie.IJK.Domain.Orders;
using Brasserie.IJK.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.IJK.Infrastructure.Persistence
{
    public class BrasserieDbContext(DbContextOptions<BrasserieDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Product> Products => Set<Product>();
    }
}
