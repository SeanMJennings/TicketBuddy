namespace Domain.Bookings.Ticket;

public interface IQueryTickets
{
    public Task<IList<TicketQuery>> GetTicketsForUser(Guid userId);
    public Task<IList<TicketQuery>> GetTicketsForEvent(Guid eventId);
}