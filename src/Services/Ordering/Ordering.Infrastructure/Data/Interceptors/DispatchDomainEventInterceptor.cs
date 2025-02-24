using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult(); ;
            return base.SavingChanges(eventData, result);
        }
        public  override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvent(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvent(DbContext? context)
        {
            if (context == null) return;

            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvent.Any())
                .Select(a => a.Entity);

            var domainEvent = aggregates
                .SelectMany(a => a.DomainEvent)
                .ToList();

            aggregates.ToList().ForEach(a=>a.ClearDomainEvents());

            foreach (var aggregate in domainEvent)
            {
                await mediator.Publish(aggregate);
            }
        }
    }
}
