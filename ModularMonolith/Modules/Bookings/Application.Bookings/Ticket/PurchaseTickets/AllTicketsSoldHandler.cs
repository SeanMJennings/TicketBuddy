using Domain.DomainEvents;
using Domain.Bookings.Ticket;
using Messages.Bookings;

namespace Application.Bookings.Ticket;

public class AllTicketsSoldHandler(IPublishMessages publish) : HandleDomainEvents<AllTicketsSold>
{
    protected override async Task Handle(AllTicketsSold message)
    {
        var integrationEvent = new EventSoldOut
        {
            EventId = message.EventId
        };
        
        await publish.Publish(integrationEvent);
    }
}