using Brasserie.IJK.Domain.Common;

namespace Brasserie.IJK.Domain.Products
{
    public class Product : AggregateRoot
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public ProductType ProductType { get; set; }

        public void IndexPrice(decimal percentage)
        {
            Price += Price * percentage;
        }
    }
}
