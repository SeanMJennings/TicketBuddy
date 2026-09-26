using Domain.DomainEvents;

namespace Domain.Bookings.Ticket;

public readonly record struct TicketWasPurchased(Guid TicketId, Guid UserId, Guid EventId) : IDescribeADomainEvent;