using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.EventsManagement;

public record EventChanged(Guid id, EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Guid venueId, Money price) : IDescribeADomainEvent;