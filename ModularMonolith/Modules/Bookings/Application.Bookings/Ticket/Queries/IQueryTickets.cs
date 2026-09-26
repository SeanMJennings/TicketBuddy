using Domain.Bookings.Ticket;

namespace Application.Bookings.Ticket.Queries;

public interface IQueryTickets
{
    public Task<IList<TicketQuery>> GetTicketsForUser(Guid userId);
    public Task<IList<TicketQuery>> GetTicketsForEvent(Guid eventId);
}