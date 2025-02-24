using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
    public record GetOrderByNameResponse(IEnumerable<OrderDto> OrderDtos);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}",
                async (string orderName, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                    var response = result.Adapt<GetOrderByNameResponse>();
                    return Results.Ok(response);
                }).WithName("GetOrderByName")
                .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Name")
                .WithDescription("Get Orders By Name")
                ;
        }
    }
}
