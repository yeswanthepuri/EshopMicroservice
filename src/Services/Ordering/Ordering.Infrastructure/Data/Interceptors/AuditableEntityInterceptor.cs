

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor :SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public async override  ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        

        public void UpdateEntities(DbContext? dbContext)
        {
            if(dbContext == null) return;

            foreach (var entity in dbContext.ChangeTracker.Entries<IEntity>())
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = "API";
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                }

                if(entity.State == EntityState.Added ||
                   entity.State == EntityState.Modified ||
                   entity.HasChangesOwnedEntities()
                    )
                {
                    entity.Entity.LastmodifiedBy = "API";
                    entity.Entity.LastmodifieddAt = DateTime.UtcNow;
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangesOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
        r.TargetEntry != null &&
        r.TargetEntry.Metadata.IsOwned() &&
        (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
