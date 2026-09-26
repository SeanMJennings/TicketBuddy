namespace Application.Bookings.Ticket.Queries;

public interface IQueryTicketReservations
{
    public Task<Dictionary<Guid, bool>> GetTicketsReservationStatusForEvent(Guid eventId, IList<Guid> ticketIds);
    public Task<string?> GetUserIdForTicketReservation(Guid eventId, Guid ticketId);
}