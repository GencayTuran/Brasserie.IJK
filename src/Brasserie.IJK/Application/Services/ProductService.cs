using Brasserie.IJK.Api.Mappers;
using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Contracts.Products;
using Brasserie.IJK.Domain.Products;
using Brasserie.IJK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.IJK.Application.Services
{
    public class ProductService(BrasserieDbContext dbContext) : IProductService
    {
        private readonly BrasserieDbContext _dbContext = dbContext;

        public async Task<Product?> GetByIdAsync(int id)
            => await _dbContext.Products.FindAsync(id);

        public async Task<IReadOnlyCollection<ProductResponse>> IndexPricesAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
            
            foreach (var p in products)
                p.IndexPrice();

            await _dbContext.SaveChangesAsync();

            return [.. products.Select(ProductMapper.ToResponse)];
        }

    }
}
