using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Contracts.Customers;
using Brasserie.IJK.Domain.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.IJK.Api.Endpoints.Customer
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/customers");

            group.MapGet("/", async (
                [FromQuery] string[] include,
                ICustomerService customerService) =>
            {
                IReadOnlyCollection<CustomerResponse> customers = include.Contains(CustomerIncludes.Orders)
                    ? await customerService.GetAllWithOrdersAsync()
                    : await customerService.GetAllAsync();

                return Results.Ok(customers);
            });

            group.MapGet("/{id:int}", async (
                int id,
                [FromQuery] string[] include,
                ICustomerService service) =>
            {
                CustomerResponse? customer = include.Contains(CustomerIncludes.Orders)
                    ? await service.GetByIdWithOrdersAsync(id)
                    : await service.GetByIdAsync(id);

                return customer is null
                    ? Results.NotFound()
                    : Results.Ok(customer);
            });

            group.MapPost("/", async (
                [FromBody] CreateCustomerRequest request,
                ICustomerService service) =>
            {
                var result = await service.CreateAsync(request);

                return result is null
                ? Results.BadRequest()
                : Results.Created("", result);
            });

            group.MapPatch("/{id:int}", async (
                int id,
                UpdateCustomerRequest request,
                ICustomerService customerService) =>
            {
                var updated = await customerService.UpdateAsync(id, request);

                return updated is null
                    ? Results.NotFound($"Customer with id {id} does not exist.")
                    : Results.Ok(updated);
            });


            group.MapDelete("/{id:int}", async (
            int id,
            ICustomerService customerService) =>
            {
                var customer = await customerService.GetByIdAsync(id);
                if (customer is null)
                    return Results.NotFound($"Customer with id {id} does not exist.");

                var deleted = await customerService.DeleteAsync(id);

                return deleted == 0
                    ? Results.BadRequest("Failed to delete customer.")
                    : Results.NoContent();
            });

            return app;
        }
    }
}
