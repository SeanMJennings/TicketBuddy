using Domain.DomainEvents;
using Domain.EventsManagement;

namespace Application.EventsManagement;

public class EventChangedHandler(
    IPublishMessages publishEndpoint) : HandleDomainEvents<EventChanged>
{
    protected override async Task Handle(EventChanged theEvent)
    {
        await publishEndpoint.Publish(new Messages.EventsManagement.EventChanged
        {
            Id = theEvent.id,
            EventName = theEvent.eventName,
            StartDate = theEvent.startDate,
            EndDate = theEvent.endDate,
            VenueId = theEvent.venueId,
            Price = theEvent.price
        });
    }
}