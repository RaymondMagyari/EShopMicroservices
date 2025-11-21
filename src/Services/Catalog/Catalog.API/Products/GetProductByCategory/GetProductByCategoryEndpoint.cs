namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryName}", async (ISender sender, string categoryName) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(categoryName));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            })             
                .WithName("ProductByCategory")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products by category")
                .WithDescription("Get Products by category");
        }
    }
}
