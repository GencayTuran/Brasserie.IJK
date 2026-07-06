using Brasserie.IJK.Api.Domain.Customers;

namespace Brasserie.IJK.Api.Domain.Orders
{
    public class Order
    {
        private readonly List<OrderLine> _orderlines = [];

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public IReadOnlyCollection<OrderLine> OrderLines => _orderlines;
    }
}
