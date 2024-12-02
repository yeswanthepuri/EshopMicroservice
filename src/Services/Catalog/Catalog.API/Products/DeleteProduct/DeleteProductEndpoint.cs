
using Mapster;
using static Catalog.API.Products.UpdateProduct.UpdateProductEndpoint;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteproductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (Guid id,ISender sender) => {

                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteproductResponse>();
                return Results.Ok(response);
            
            })
        .WithName("DeleteProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
        }
    }
}
