using Brasserie.IJK.Contracts.Customers;
using Brasserie.IJK.Contracts.Products;
using Brasserie.IJK.Domain.Products;

namespace Brasserie.IJK.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<ProductResponse>> IndexPricesAsync();
    }
}
