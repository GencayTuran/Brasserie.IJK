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
            return Customer.Create(
                request.PersonalInfo,
                request.ContactInfo,
                request.AddressInfo
            );
        }
    }
}
