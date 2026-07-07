using Brasserie.IJK.Contracts.Customers;

namespace Brasserie.IJK.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IReadOnlyCollection<CustomerResponse>> GetAllWithOrdersAsync();
        Task<CustomerResponse?> GetByIdWithOrdersAsync(int id);

        Task<IReadOnlyCollection<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse?> GetByIdAsync(int id);
        Task<CustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<CustomerResponse> UpdateAsync(int id, UpdateCustomerRequest request);
        Task<int> DeleteAsync(int id);
    }
}
