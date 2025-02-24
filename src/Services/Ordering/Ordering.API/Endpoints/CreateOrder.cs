using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto order);
    public record CreateOrderResponse(Guid ID);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async ([FromBody] CreateOrderRequest request, ISender sender) =>
            {
                var command = new CreateOrderCommand(request.order);
                var result = await sender.Send(command);

                var response = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.ID}", response);
            })
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order")
                ;
        }
    }
}
