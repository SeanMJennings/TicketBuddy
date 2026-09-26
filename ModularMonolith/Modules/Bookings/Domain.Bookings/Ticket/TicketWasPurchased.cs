using Domain.DomainEvents;

namespace Domain.Bookings.Ticket;

public record TicketWasPurchased(Guid TicketId, Guid UserId, Guid EventId) : IDescribeADomainEvent;