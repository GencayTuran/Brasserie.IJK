using Brasserie.IJK.Domain.Common;
using Brasserie.IJK.Domain.Orders;

namespace Brasserie.IJK.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public required PersonalInfo PersonalInfo { get; set; }

        public required ContactInfo ContactInfo { get; set; }

        public required AddressInfo AddressInfo { get; set; }

        public ICollection<Order> Orders { get; set; } = [];
    }
}
