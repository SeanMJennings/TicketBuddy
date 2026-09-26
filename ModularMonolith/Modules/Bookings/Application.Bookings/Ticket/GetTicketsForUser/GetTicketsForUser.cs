using Domain.Bookings.Ticket;

namespace Application.Bookings.Ticket.GetTicketsForUser;

public class GetTicketsForUser(IQueryTickets ticketQuerist)
{
    public async Task<IList<TicketQuery>> Execute(Guid userId)
    {
        return await ticketQuerist.GetTicketsForUser(userId);
    }
}