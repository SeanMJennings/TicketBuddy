using Domain.Aggregates;
using Domain.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DomainEventsDispatching;

public static class DomainEventsAccessor
{
    public static IReadOnlyCollection<IDescribeADomainEvent> GetAllDomainEvents(DbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Aggregate>()
            .Where(x => x.Entity.DomainEvents.Count != 0).ToList();

        return domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
    }

    public static void ClearAllDomainEvents(DbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Aggregate>()
            .Where(x => x.Entity.DomainEvents.Count != 0).ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}