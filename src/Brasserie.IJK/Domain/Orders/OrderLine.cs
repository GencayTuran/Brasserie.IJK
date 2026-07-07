using Brasserie.IJK.Domain.Common;
using Brasserie.IJK.Domain.Products;

namespace Brasserie.IJK.Domain.Orders
{
    public class OrderLine : Entity
    {
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal CalculateLineTotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
