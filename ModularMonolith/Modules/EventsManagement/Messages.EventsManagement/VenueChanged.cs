namespace Messages.EventsManagement;

public record VenueChanged
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public uint Capacity { get; init; }
}
