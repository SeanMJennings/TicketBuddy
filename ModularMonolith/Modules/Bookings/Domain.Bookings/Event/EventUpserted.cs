using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.Bookings.Event;

public record EventUpserted(Guid EventId, Money Price, Guid VenueId) : IDescribeADomainEvent;