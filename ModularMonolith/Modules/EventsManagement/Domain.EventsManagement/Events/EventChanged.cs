using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.EventsManagement;

public record EventChanged(Guid Id, EventName EventName, DateTimeOffset StartDate, DateTimeOffset EndDate, Guid VenueId, Money Price)  : IDescribeADomainEvent;