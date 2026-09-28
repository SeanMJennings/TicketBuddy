using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Bookings.Event;

public class Event : Aggregate
{
    private Event(Guid id, EventName eventName, Guid venueId, Money price) : base(id)
    {
        EventName = eventName;
        VenueId = venueId;
        Price = price;
    }
    
    public static Event Create(Guid id, EventName eventName, Guid venueId, Money price)
    {
        var newEvent = new Event(id, eventName, venueId, price);
        newEvent.RaiseEventUpsertedDomainEvent();
        return newEvent;
    }
    
    public EventName EventName { get; private set; }
    public Money Price { get; private set; }
    public Guid VenueId { get; private set; }
    
    public void UpdateName(EventName eventName) => EventName = eventName;
    
    public void UpdateVenue(Guid venueId) => VenueId = venueId;
    
    public void UpdatePrice(Money price) => Price = price;

    private void RaiseEventUpsertedDomainEvent() => AddDomainEvent(new EventChanged(Id, Price, VenueId));
}