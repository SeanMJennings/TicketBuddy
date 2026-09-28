using Domain.EventsManagement;
using Infrastructure.EventsManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventsManagement.Event;

public class EventRepository(EventManagementDbContext eventManagementDbContext) : IPersistEvents
{
    public void Add(Domain.EventsManagement.Event theEvent)
    {
        eventManagementDbContext.Add(theEvent);
    }

    public void Update(Domain.EventsManagement.Event @event)
    {
        eventManagementDbContext.Update(@event);
    }

    public async Task<Domain.EventsManagement.Event?> Get(Guid id)
    {
        return await eventManagementDbContext.Events.FindAsync(id);
    }

    public async Task<IList<Domain.EventsManagement.Event>> GetAll()
    {
        return await eventManagementDbContext.Events
            .Where(e => e.StartDate > DateTimeOffset.UtcNow)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }
}