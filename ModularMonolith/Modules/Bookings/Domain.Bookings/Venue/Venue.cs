using Domain.Entities;

namespace Domain.Bookings.Venue;

public class Venue(Guid id, string name, uint capacity) : Entity(id), IAmAnAggregateRoot
{
    public string Name { get; init; } = name;
    public uint Capacity { get; init; } = capacity;
}