﻿using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto OrderDto);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async ([FromBody] UpdateOrderRequest request, ISender sender) =>
            {
                var command = new UpdateOrderCommand(request.OrderDto);
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            }).WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Order")
                .WithDescription("Update Order")
                ;
        }
    }
}
