using Domain.Bookings.Event;
using Infrastructure.Bookings.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Bookings.Event;

public class EventRepository(BookingDbContext bookingDbContext) : IPersistEvents
{
    public async Task Upsert(Domain.Bookings.Event.Event theEvent)
    {
        var @event = await GetById(theEvent.Id);
        
        if (@event is not null)
        {
            UpdateEvent(theEvent, @event);
            return;
        }

        AddEvent(theEvent);
    }

    private void AddEvent(Domain.Bookings.Event.Event theEvent)
    {
        bookingDbContext.Add(theEvent);
    }

    private void UpdateEvent(Domain.Bookings.Event.Event theEvent, Domain.Bookings.Event.Event @event)
    {
        @event.UpdateName(theEvent.EventName);
        @event.UpdateVenue(theEvent.VenueId);
        @event.UpdatePrice(theEvent.Price);
        @event.TransferDomainEventsFrom(theEvent);
        bookingDbContext.Update(@event);
    }

    public async Task<Domain.Bookings.Event.Event?> GetById(Guid id)
    {
        return await bookingDbContext.Events
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}

