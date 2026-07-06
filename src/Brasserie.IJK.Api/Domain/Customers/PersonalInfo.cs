using Brasserie.IJK.Api.Domain.Shared;

namespace Brasserie.IJK.Api.Domain.Customers
{
    public record PersonalInfo
    {
        public required string FirstName { get; init; }

        public required string LastName { get; init; }

        public required Gender Gender { get; init; }

        public required DateOnly DateOfBirth { get; init; }
    }
}
