using Brasserie.IJK.Api.Domain.Common;
using Brasserie.IJK.Api.Domain.Customers;

namespace Brasserie.IJK.Api.Domain.Orders
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
