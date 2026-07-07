using Brasserie.IJK.Contracts.Customers;
using Brasserie.IJK.Domain.Customers;

namespace Brasserie.IJK.Api.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerResponse ToResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                PersonalInfo = customer.PersonalInfo,
                ContactInfo = customer.ContactInfo,
                AddressInfo = customer.AddressInfo,

                Orders = [.. 
                    customer.Orders.Select(OrderMapper.ToResponse)
                ]
            };
        }

        public static Customer ToDomain(CreateCustomerRequest request)
        {
            return new Customer
            {
                PersonalInfo = request.PersonalInfo,
                ContactInfo = request.ContactInfo,
                AddressInfo = request.AddressInfo,
                Orders = []
            };
        }

        internal static void UpdateDomain(Customer customer, UpdateCustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
