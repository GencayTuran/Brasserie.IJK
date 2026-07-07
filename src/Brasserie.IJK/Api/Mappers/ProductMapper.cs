using Brasserie.IJK.Contracts.Products;
using Brasserie.IJK.Domain.Products;

namespace Brasserie.IJK.Api.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                QuantityInStock = product.QuantityInStock,
                Price = product.Price,
                ProductType = product.ProductType
            };
        }
    }
}
