﻿
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProduct
{
    public class GetProductEndpoint : ICarterModule
    {
        public record GetProductByIdResponse(Product Product);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id"); ;
        }
    }
}
