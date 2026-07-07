using Brasserie.IJK.Domain.Common;
using Brasserie.IJK.Domain.Customers;
using Brasserie.IJK.Domain.Products;
using Brasserie.IJK.Domain.Shared;

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

        public decimal CalculateSubtotal()
        {
            return _orderlines.Sum(x => x.Quantity * x.UnitPrice);
        }

        public decimal CalculateVatAmount(VatRate rate)
        {
            return _orderlines.Sum(x => x.Quantity * x.UnitPrice) * ((decimal)rate / 100m);
        }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }

        public void SetOrderDate()
        {
            OrderDate = DateTime.UtcNow;
        }

        public void AddOrderLine(Product product, int quantity)
        {
            _orderlines.Add(new OrderLine 
            { 
                Product = product,
                ProductId = product.Id,
                Quantity = quantity,
                UnitPrice = product.Price
            });
        }
    }
}
