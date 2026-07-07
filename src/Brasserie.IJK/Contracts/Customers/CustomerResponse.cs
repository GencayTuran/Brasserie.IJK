using Brasserie.IJK.Contracts.Orders;
using Brasserie.IJK.Domain.Customers;

namespace Brasserie.IJK.Contracts.Customers
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public required PersonalInfo PersonalInfo { get; set; }

        public required ContactInfo ContactInfo { get; set; }

        public required AddressInfo AddressInfo { get; set; }

        public IReadOnlyCollection<OrderResponse>? Orders { get; set; }
    }

}
