using Domain.DomainEvents;
using Domain.EventsManagement.Venue;

namespace Application.EventsManagement.Venue;

public class VenueChangedHandler(
    IPublishMessages publishEndpoint) : HandleDomainEvents<VenueChanged>
{
    protected override async Task Handle(VenueChanged theVenue)
    {
        await publishEndpoint.Publish(new Messages.EventsManagement.VenueChanged
        {
            Id = theVenue.id,
            Name = theVenue.name,
            Capacity = theVenue.capacity,
        });
    }
}