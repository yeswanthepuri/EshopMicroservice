

using Ordering.Domain.Models;

namespace Ordering.Domain.Event
{
    public record OrderUpdateEvent(Order Order) : IDomainEvent;

}
