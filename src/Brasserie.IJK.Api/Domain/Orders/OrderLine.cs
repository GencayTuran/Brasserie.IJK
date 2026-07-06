using Brasserie.IJK.Api.Domain.Common;
using Brasserie.IJK.Api.Domain.Products;

namespace Brasserie.IJK.Api.Domain.Orders
{
    public class OrderLine : Entity
    {
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
