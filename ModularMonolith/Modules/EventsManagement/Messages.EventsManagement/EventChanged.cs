namespace Messages.EventsManagement;

public record EventChanged
{
    public Guid Id { get; init; }
    public string EventName { get; init; } = null!;
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public Guid VenueId { get; init; }
    public decimal Price { get; init; }
}