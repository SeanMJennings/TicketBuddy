using Domain.DomainEvents;

namespace Domain.EventsManagement.Venue;

public record VenueChanged(Guid id, VenueName name, Address address, uint capacity) : IDescribeADomainEvent;