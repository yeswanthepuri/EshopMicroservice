using Ordering.Domain.Models;


namespace Ordering.Domain.Event
{
    public record OrderCreateEvent(Order Order) : IDomainEvent;
}
