using Brasserie.IJK.Application.Interfaces;

namespace Brasserie.IJK.Api.Endpoints.Product
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");

            group.MapPost("/index-prices", async (
                IProductService productService) =>
            {
                var result = await productService.IndexPricesAsync();
                return Results.Ok(result);
            });

            return app;
        }
    }
}
