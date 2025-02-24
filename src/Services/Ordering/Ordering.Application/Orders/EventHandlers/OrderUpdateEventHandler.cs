using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Event;


namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdateEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdateEvent>
    {
        public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled:{DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
