using Brasserie.IJK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.IJK.Api.Endpoints.Product
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");

            group.MapGet("/", async (IProductService productService) =>
            {
                var result = await productService.GetAllAsync();
                return Results.Ok(result);
            });


            group.MapPost("/", async (
                [FromQuery] string indexBy,
                IProductService productService) =>
            {
                if (!decimal.TryParse(indexBy, out decimal percentage))
                    Results.BadRequest($"Invalid value for parameter {nameof(indexBy)}.");

                var result = await productService.IndexPricesAsync(percentage);

                return Results.Ok(result);
            });

            return app;
        }
    }
}
