using Brasserie.IJK.Domain.Shared;

namespace Brasserie.IJK.Contracts.Customers
{
    public class UpdateCustomerRequest
    {
        public  string? FirstName { get; set; }

        public  string? LastName { get; set; }

        public  Gender? Gender { get; set; }



        public  string? EmailAddress { get; set; }

        public  string? PhoneNumber { get; set; }



        public  string? Street { get; set; }

        public  string? HouseNumber { get; set; }

        public  string? PostalCode { get; set; }

        public  string? City { get; set; }

        public  string? Country { get; set; }
    }
}
