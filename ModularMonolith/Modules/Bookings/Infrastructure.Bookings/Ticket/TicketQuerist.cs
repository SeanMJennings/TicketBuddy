using Domain.Bookings.Ticket;
using Infrastructure.Queries;

namespace Infrastructure.Bookings.Ticket;

public class TicketQuerist(Database database) : IQueryTickets
{
    public async Task<IList<TicketQuery>> GetTicketsForEvent(Guid eventId)
    {
        return (await database.Query<TicketQuery>("""
                                                  SELECT "Id", "EventId", "Price", "SeatNumber", ("PurchasedAt" IS NOT NULL) AS "Purchased"
                                                  FROM "Booking"."Tickets"
                                                  WHERE "EventId" = @EventId
                                                  """, new { EventId = eventId })).ToList();
    }
    
        
    public async Task<IList<TicketQuery>> GetTicketsForUser(Guid userId)
    {
        return (await database.Query<TicketQuery>("""
                                                  SELECT "Id", "EventId", "Price", "SeatNumber", ("PurchasedAt" IS NOT NULL) AS "Purchased"
                                                  FROM "Booking"."Tickets"
                                                  WHERE "UserId" = @UserId
                                                  """, new { UserId = userId })).ToList();
    }
}