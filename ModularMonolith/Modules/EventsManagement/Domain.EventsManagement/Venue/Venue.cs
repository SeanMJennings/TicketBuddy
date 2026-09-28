using System.Text.Json.Serialization;
using Domain.Aggregates;

namespace Domain.EventsManagement.Venue;

public class Venue : Aggregate
{
    private const uint MinCapacity = 1;
    private const uint MaxCapacity = 50;

    private Venue() : base(Guid.CreateVersion7()) { }

    [JsonConstructor]
    private Venue(Guid id, VenueName name, Address address, uint capacity) : base(id)
    {
        ValidateCapacity(capacity);

        Name = name;
        Address = address;
        Capacity = capacity;
    }

    public static Venue Create(Guid id, VenueName name, Address address, uint capacity)
    {
        var venue = new Venue(id, name, address, capacity);
        venue.RaiseVenueChangedDomainEvent();
        return venue;
    }

    public VenueName Name { get; private set; }
    public Address Address { get; private set; }
    public uint Capacity { get; private set; }

    public void UpdateName(VenueName name)
    {
        Name = name;
        RaiseVenueChangedDomainEvent();
    }

    public void UpdateAddress(Address address)
    {
        Address = address;
        RaiseVenueChangedDomainEvent();
    }

    public void UpdateCapacity(uint capacity)
    {
        ValidateCapacity(capacity);
        Capacity = capacity;
        RaiseVenueChangedDomainEvent();
    }

    private static void ValidateCapacity(uint capacity)
    {
        Validation.BasedOn(errors =>
        {
            if (capacity < MinCapacity) errors.Add("Capacity must be at least 1");
            if (capacity > MaxCapacity) errors.Add("Capacity cannot exceed 50 seats");
        });
    }

    private void RaiseVenueChangedDomainEvent() => AddDomainEvent(new VenueChanged(Id, Name, Address, Capacity));
}