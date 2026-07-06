using Brasserie.IJK.Api.Domain.Common;
using Brasserie.IJK.Api.Domain.Orders;

namespace Brasserie.IJK.Api.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public required PersonalInfo PersonalInfo { get; set; }

        public required ContactInfo ContactInfo { get; set; }

        public required AddressInfo AddressInfo { get; set; }

        public ICollection<Order> Orders { get; set; } = [];
    }
}
