using Domain.Aggregates;

namespace Domain.Bookings.Venue;

public class Venue(Guid id, string name, uint capacity) : Aggregate(id)
{
    public string Name { get; init; } = name;
    public uint Capacity { get; init; } = capacity;
}