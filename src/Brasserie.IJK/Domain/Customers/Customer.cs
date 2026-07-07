using Brasserie.IJK.Domain.Common;
using Brasserie.IJK.Domain.Orders;

namespace Brasserie.IJK.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public PersonalInfo PersonalInfo { get; private set; } = null!;

        public ContactInfo ContactInfo { get; private set; } = null!;

        public AddressInfo AddressInfo { get; private set; } = null!;

        public ICollection<Order> Orders { get; set; } = [];

        private Customer() {}

        public static Customer Create(
            PersonalInfo personalInfo,
            ContactInfo contactInfo,
            AddressInfo addressInfo)
        {
            var customer = new Customer
            {
                PersonalInfo = personalInfo,
                ContactInfo = contactInfo,
                AddressInfo = addressInfo
            };

            return customer;
        }

        public void UpdatePersonalInfo(PersonalInfo info)
            => PersonalInfo = info;

        public void UpdateContactInfo(ContactInfo info)
            => ContactInfo = info;

        public void UpdateAddressInfo(AddressInfo info)
            => AddressInfo = info;
    }
}
