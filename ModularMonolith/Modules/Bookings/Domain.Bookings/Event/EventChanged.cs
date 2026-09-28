using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.Bookings.Event;

public record EventChanged(Guid EventId, Money Price, Guid VenueId) : IDescribeADomainEvent;