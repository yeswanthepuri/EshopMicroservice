
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCatagory
{
    public class GetProductsByCatagoryEndpoint : ICarterModule
    {
        public record GetProductResponse(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/catagory/{catagory}", async (string catagory,ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCatagoryQuery(catagory));

                var response = result.Adapt<GetProductsByCatagoryResult>();

                return Results.Ok( response);
            })
                .WithName("GetProductByCatagory")
                .Produces<GetProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product List")
                .WithDescription("Get Product List");
        }
    }
}
