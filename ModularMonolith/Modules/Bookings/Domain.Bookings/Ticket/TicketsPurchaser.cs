using System.ComponentModel.DataAnnotations;

namespace Domain.Bookings.Ticket;

public static class TicketsPurchaser
{
    public static async Task PurchaseTickets(Guid eventId, Guid userId, IReadOnlyList<Ticket> tickets, IPersistTickets ticketRepository)
    {
        if (tickets.Any(t => t.EventId != eventId)) throw new ValidationException("One or more tickets do not belong to this event");

        foreach (var ticket in tickets) ticket.Purchase(userId);

        await ticketRepository.UpdateRange(tickets);
    }
}