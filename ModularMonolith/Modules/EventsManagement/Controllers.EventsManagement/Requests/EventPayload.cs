using Domain.ValueObjects;

namespace Controllers.EventsManagement.Requests;

public record EventPayload(EventName EventName, DateTimeOffset StartDate, DateTimeOffset EndDate, Guid VenueId, Money Price);