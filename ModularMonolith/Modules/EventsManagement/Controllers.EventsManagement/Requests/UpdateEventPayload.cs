using Domain.ValueObjects;

namespace Controllers.EventsManagement.Requests;

public record UpdateEventPayload(EventName EventName, DateTimeOffset StartDate, DateTimeOffset EndDate, decimal Price);