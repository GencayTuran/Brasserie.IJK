using Brasserie.IJK.Domain.Products;

namespace Brasserie.IJK.Contracts.Orders
{
    public class CreateOrderLineDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}