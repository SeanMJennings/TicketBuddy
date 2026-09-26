using Domain.DomainEvents;

namespace Domain.Bookings.Ticket;

public record AllTicketsSold(Guid EventId) : IDescribeADomainEvent;