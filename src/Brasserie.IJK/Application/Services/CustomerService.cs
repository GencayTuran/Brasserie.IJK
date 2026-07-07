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

            var (updatedPersonalInfo, updatedContactInfo, updatedAddressInfo) = 
                PrepareUpdatedCustomerRecords(customer, request);

            customer.UpdateAddressInfo(updatedAddressInfo);
            customer.UpdateContactInfo(updatedContactInfo);
            customer.UpdatePersonalInfo(updatedPersonalInfo);

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

        private static (PersonalInfo, ContactInfo, AddressInfo) PrepareUpdatedCustomerRecords(Customer customer, UpdateCustomerRequest request)
        {
            var updatedPersonalInfo = customer.PersonalInfo with
            {
                FirstName = request.FirstName ?? customer.PersonalInfo.FirstName,
                LastName = request.LastName ?? customer.PersonalInfo.LastName,
                Gender = request.Gender ?? customer.PersonalInfo.Gender
            };

            var updatedAddressInfo = customer.AddressInfo with
            {
                Street = request.Street ?? customer.AddressInfo.Street,
                HouseNumber = request.HouseNumber ?? customer.AddressInfo.HouseNumber,
                City = request.City ?? customer.AddressInfo.City,
                PostalCode = request.PostalCode ?? customer.AddressInfo.PostalCode,
                Country = request.Country ?? customer.AddressInfo.Country
            };

            var updatedContactInfo = customer.ContactInfo with
            {
                EmailAddress = request.EmailAddress ?? customer.ContactInfo.EmailAddress,
                PhoneNumber = request.PhoneNumber ?? customer.ContactInfo.PhoneNumber,
            };

            return (updatedPersonalInfo, updatedContactInfo, updatedAddressInfo);
        }
    }
}
