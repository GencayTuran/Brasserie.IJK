using Brasserie.IJK.Domain.Common;
using Brasserie.IJK.Domain.Customers;

namespace Brasserie.IJK.Domain.Orders
{
    public class Order : AggregateRoot
    {
        private readonly List<OrderLine> _orderlines = [];

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public IReadOnlyCollection<OrderLine> OrderLines => _orderlines;
    }
}
