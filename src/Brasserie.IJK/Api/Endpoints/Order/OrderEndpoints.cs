using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.IJK.Api.Endpoints.Order
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/orders");

            group.MapPost("/", async (
                    [FromBody] CreateOrderRequest request,
                    IOrderService orderService,
                    ICustomerService customerService,
                    IProductService productService) =>
            {
                //  customer exists?
                var customer = await customerService.GetByIdAsync(request.CustomerId);
                if (customer is null)
                    return Results.BadRequest($"Customer with id {request.CustomerId} does not exist.");

                // products exist?
                var products = new List<Domain.Products.Product>();
                foreach (var line in request.OrderLines)
                {
                    var product = await productService.GetByIdAsync(line.ProductId);
                    if (product is null)
                        return Results.BadRequest("Some of the products in the order do not exist.");

                    products.Add(product);
                }

                // qty check
                foreach (var line in request.OrderLines)
                {
                    var product = products.First(p => p.Id == line.ProductId);
                    if (product.QuantityInStock < line.Quantity)
                        return Results.BadRequest($"Product {product.Name} does not have enough stock.");
                }

                var orderResonse = await orderService.CreateAsync(request, products);

                return orderResonse is null
                ? Results.BadRequest()
                : Results.Created("", orderResonse);
            });

            group.MapPatch("/{id:int}", async (
                int id,
                UpdateOrderRequest request,
                IOrderService orderService) =>
            {
                if (string.IsNullOrEmpty(request.Status))
                    return Results.BadRequest("Body of update request is invalid");

                var updated = await orderService.UpdateAsync(id, request);

                return updated is null
                    ? Results.BadRequest($"Order with id {id} does not exist or body of the request is invalid.") 
                    : Results.Ok(updated);
            });


            group.MapDelete("/{id:int}", async (
            int id,
            IOrderService orderService) =>
            {
                var deleted = await orderService.DeleteAsync(id);

                return deleted == 0
                    ? Results.NotFound($"Order with id {id} does not exist.")
                    : Results.NoContent();
            });

            group.MapGet("/{id:int}/receipt", async (
                int id,
                IOrderService orderService) =>
            {
                var receipt = await orderService.GetReceiptAsync(id);

                return receipt is null
                    ? Results.NotFound($"Order with id {id} does not exist.")
                    : Results.Ok(receipt);
            });


            return app;
        }
    }
}
