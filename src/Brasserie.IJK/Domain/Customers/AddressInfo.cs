namespace Brasserie.IJK.Domain.Customers
{
    public record AddressInfo
    {
        public required string Street { get; init; }

        public required string HouseNumber { get; init; }

        public required string PostalCode { get; init; }

        public required string City { get; init; }

        public required string Country { get; init; }
    }
}
