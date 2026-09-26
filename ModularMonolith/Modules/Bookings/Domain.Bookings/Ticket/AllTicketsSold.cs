using Domain.DomainEvents;

namespace Domain.Bookings.Ticket;

public readonly record struct AllTicketsSold(Guid EventId) : IDescribeADomainEvent;