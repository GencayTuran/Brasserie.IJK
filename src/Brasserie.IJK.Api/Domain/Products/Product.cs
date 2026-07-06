using Brasserie.IJK.Api.Domain.Common;

namespace Brasserie.IJK.Api.Domain.Products
{
    public class Product : AggregateRoot
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public ProductType ProductType { get; set; }
    }
}
