using Microsoft.EntityFrameworkCore;

namespace Brasserie.IJK.Domain.Customers
{
    [Owned]
    public record ContactInfo
    {
        public required string EmailAddress { get; init; }

        public required string PhoneNumber { get; init; }
    }
}
