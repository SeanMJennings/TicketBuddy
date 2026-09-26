namespace Messages.EventsManagement;

public record VenueUpserted
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public uint Capacity { get; init; }
}
