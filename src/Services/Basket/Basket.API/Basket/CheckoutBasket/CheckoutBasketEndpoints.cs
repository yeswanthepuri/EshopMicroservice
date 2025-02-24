using Basket.API.Basket.GetBasket;
using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
    public record CheckoutBaskatResponse(bool IsSuccess);
    public class CheckoutBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutBaskatResponse>();

                return Results.Ok(response);
            })
         .WithName("CreateBasket")
        .Produces<CheckoutBaskatResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket"); ;
        }
    }
}
