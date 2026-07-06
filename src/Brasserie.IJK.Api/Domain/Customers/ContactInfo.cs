namespace Brasserie.IJK.Api.Domain.Customers
{
    public record ContactInfo
    {
        public required string EmailAddress { get; init; }

        public required string Phone { get; init; }
    }
}
