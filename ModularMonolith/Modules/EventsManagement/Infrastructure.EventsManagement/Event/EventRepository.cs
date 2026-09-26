using Application;
using Domain.EventsManagement;
using Infrastructure.EventsManagement.Core;
using Messages.EventsManagement;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventsManagement.Event;

public class EventRepository(EventManagementDbContext eventManagementDbContext, IPublishMessages publishEndpoint) : IPersistEvents
{
    public async Task Add(Domain.EventsManagement.Event theEvent)
    {
        eventManagementDbContext.Add(theEvent);
        await PublishEventUpserted(theEvent);
    }

    public async Task Update(Domain.EventsManagement.Event @event)
    {
        eventManagementDbContext.Update(@event);
        await PublishEventUpserted(@event);
    }

    private async Task PublishEventUpserted(Domain.EventsManagement.Event theEvent)
    {
        await publishEndpoint.Publish(new EventUpserted
        {
            Id = theEvent.Id,
            EventName = theEvent.EventName,
            StartDate = theEvent.StartDate,
            EndDate = theEvent.EndDate,
            VenueId = theEvent.VenueId,
            Price = theEvent.Price
        });
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