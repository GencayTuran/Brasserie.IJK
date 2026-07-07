using Brasserie.IJK.Domain.Customers;
using Brasserie.IJK.Domain.Orders;

namespace Brasserie.IJK.Contracts.Orders
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }

        public ICollection<CreateOrderLineDto> OrderLines { get; set; } = [];
    }
}
