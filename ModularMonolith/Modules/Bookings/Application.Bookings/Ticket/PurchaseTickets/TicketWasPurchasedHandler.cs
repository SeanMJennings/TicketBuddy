using Domain.DomainEvents;
using Domain.Bookings.Event;
using Domain.Bookings.Ticket;
using Messages.Bookings;

namespace Application.Bookings.Ticket.PurchaseTickets;

public class TicketWasPurchasedHandler(
    IPublishMessages publish,
    IPersistEvents eventRepository) : HandleDomainEvents<TicketWasPurchased>
{
    protected override async Task Handle(TicketWasPurchased message)
    {
        var theEvent = await eventRepository.GetById(message.EventId)!;

        if (theEvent is null) return;
        
        var integrationEvent = new TicketPurchased
        {
            TicketId = message.TicketId,
            UserId = message.UserId,
            EventId = message.EventId,
            EventName = theEvent.EventName
        };

        await publish.Publish(integrationEvent);
    }
}