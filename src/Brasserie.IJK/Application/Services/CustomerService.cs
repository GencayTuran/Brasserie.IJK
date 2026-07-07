using Brasserie.IJK.Api.Mappers;
using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Contracts.Customers;
using Brasserie.IJK.Domain.Customers;
using Brasserie.IJK.Domain.Products;
using Brasserie.IJK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Brasserie.IJK.Application.Services
{
    public class CustomerService(BrasserieDbContext dbContext) : ICustomerService
    {
        private readonly BrasserieDbContext _dbContext = dbContext;

        public async Task<IReadOnlyCollection<CustomerResponse>> GetAllAsync()
        {
            return await _dbContext.Customers
                .Select(x => CustomerMapper.ToResponse(x))
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<CustomerResponse>> GetAllWithOrdersAsync()
        {
            var customers = await GetCustomerRelations()
                .ToListAsync();

            return [.. customers.Select(CustomerMapper.ToResponse)];
        }

        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            return customer is not null
                ? CustomerMapper.ToResponse(customer)
                : null;
        }

        public async Task<CustomerResponse?> GetByIdWithOrdersAsync(int id)
        {
            var customer = await GetCustomerRelations()
                .FirstOrDefaultAsync(x => x.Id == id);

            return customer is not null
                ? CustomerMapper.ToResponse(customer)
                : null;
        }

        public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request)
        {
            var newCustomer = CustomerMapper.ToDomain(request);
            var createdCustomer = (await _dbContext.AddAsync(newCustomer)).Entity;

            await _dbContext.SaveChangesAsync();
            return CustomerMapper.ToResponse(createdCustomer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer is null)
                return 0;

            _dbContext.Customers.Remove(customer);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<CustomerResponse?> UpdateAsync(int id, UpdateCustomerRequest request)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer is null)
                return null;

            CustomerMapper.UpdateDomain(customer, request);

            await _dbContext.SaveChangesAsync();
            return CustomerMapper.ToResponse(customer);
        }


        private IIncludableQueryable<Customer, Product> GetCustomerRelations()
        {
            return _dbContext.Customers
                .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderLines)
                        .ThenInclude(x => x.Product);
        }
    }
}
