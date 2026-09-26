using Domain.ValueObjects;

namespace Domain.Bookings.Ticket;

public static class TicketsReleaser
{
    public static async Task ReleaseTicketsForEvent(Guid eventId, Money price, uint venueCapacity, IPersistTickets ticketRepository)
    {
        var tickets = new List<Ticket>();
        for (uint i = 0; i < venueCapacity; i++)
        {
            var ticket = new Ticket(
                Guid.CreateVersion7(),
                eventId,
                price,
                i + 1);
            tickets.Add(ticket);
        }

        await ticketRepository.AddRange(tickets);
    }
}