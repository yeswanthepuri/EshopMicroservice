using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreateEvent>
    {
        public Task Handle(OrderCreateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled:{DomainEvent}",notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
