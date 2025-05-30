﻿

using Basket.API.Models;

namespace Basket.API.Basket.GetBasket
{

    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));


                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
                .WithName("CreateProduct")
         .WithName("GetProductById")
        .Produces<GetBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
        }
    }
}
