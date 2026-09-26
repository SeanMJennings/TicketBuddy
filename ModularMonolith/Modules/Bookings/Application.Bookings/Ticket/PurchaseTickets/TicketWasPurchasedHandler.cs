using Domain.DomainEvents;
using Domain.Bookings.Event;
using Domain.Bookings.Ticket;
using Messages.Bookings;

namespace Application.Bookings.Ticket.PurchaseTickets;

public class TicketWasPurchasedHandler(
    IPublishMessages publish,
    IPersistEvents eventRepository,
    IPersistTickets ticketRepository) : HandleDomainEvents<TicketWasPurchased>
{
    protected override async Task Handle(TicketWasPurchased message)
    {
        var theEvent = await eventRepository.GetById(message.EventId)!;
        
        var availableCount = await ticketRepository.GetAvailableCountByEventId(message.EventId);
        
        var ticketPurchasedIntegrationEvent = new TicketPurchased
        {
            TicketId = message.TicketId,
            UserId = message.UserId,
            EventId = message.EventId,
            EventName = theEvent!.EventName
        };

        await publish.Publish(ticketPurchasedIntegrationEvent);
        
        if (availableCount == 0)
        {
            var soldOutIntegrationEvent = new EventSoldOut
            {
                EventId = message.EventId
            };
        
            await publish.Publish(soldOutIntegrationEvent);
        }
    }
}