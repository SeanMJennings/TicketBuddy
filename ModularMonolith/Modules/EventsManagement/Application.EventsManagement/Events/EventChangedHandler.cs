using Domain.DomainEvents;
using Domain.EventsManagement;
using Messages.EventsManagement;

namespace Application.EventsManagement;

public class EventChangedHandler(IPublishMessages publishEndpoint) : HandleDomainEvents<EventChanged>
{
    protected override async Task Handle(EventChanged message)
    {
        await publishEndpoint.Publish(new EventUpserted
        {
            Id = message.Id,
            EventName = message.EventName,
            StartDate = message.StartDate,
            EndDate = message.EndDate,
            VenueId = message.VenueId,
            Price = message.Price
        });
    }
}