using Domain.Bookings.Ticket;
using Infrastructure.Bookings.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Bookings.Ticket;

public class TicketRepository(BookingDbContext bookingDbContext) : IPersistTickets
{
    public async Task<IReadOnlyList<Domain.Bookings.Ticket.Ticket>> GetByIds(Guid[] ids)
    {
        return await bookingDbContext.Tickets
            .Where(t => ((IEnumerable<Guid>)ids).Contains(t.Id))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Domain.Bookings.Ticket.Ticket>> GetByEventId(Guid eventId)
    {
        return await bookingDbContext.Tickets
            .Where(t => t.EventId == eventId)
            .ToListAsync();
    }

    public async Task<int> GetAvailableCountByEventId(Guid eventId)
    {
        return await bookingDbContext.Tickets
            .Where(t => t.EventId == eventId && t.UserId == null)
            .CountAsync();
    }

    public async Task AddRange(IEnumerable<Domain.Bookings.Ticket.Ticket> tickets)
    {
        await bookingDbContext.Tickets.AddRangeAsync(tickets);
    }

    public Task UpdateRange(IEnumerable<Domain.Bookings.Ticket.Ticket> tickets)
    {
        foreach (var ticket in tickets)
        {
            bookingDbContext.Entry(ticket).State = EntityState.Modified;
        }
        return Task.CompletedTask;
    }
}