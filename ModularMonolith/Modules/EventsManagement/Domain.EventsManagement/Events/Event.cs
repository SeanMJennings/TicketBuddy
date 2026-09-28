using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.EventsManagement;

public class Event : Aggregate
{
    [JsonConstructor]
    private Event(Guid id, EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Guid venueId, Money price) : base(id)
    {
        if (endDate < startDate) throw new ValidationException("End date cannot be before start date");
        EventName = eventName;
        StartDate = startDate;
        EndDate = endDate;
        VenueId = venueId;
        Price = price;
    }    
    
    public static Event Create(Guid id, EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Guid venueId, Money price)
    {
        var theEvent = new Event(id, eventName, startDate, endDate, venueId, price);
        theEvent.RaiseEventUpsertedDomainEvent();
        return theEvent;
    }

    public EventName EventName { get; private set; }
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public Guid VenueId { get; private set; }
    public Money Price { get; private set; }
    
    [JsonInclude]
    public bool IsSoldOut { get; private set; }

    public void UpdateName(EventName eventName)
    {
        EventName = eventName;
        RaiseEventUpsertedDomainEvent();
    }
    public void UpdateDates(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate < DateTimeOffset.UtcNow || endDate < DateTimeOffset.UtcNow) throw new ValidationException("Event date cannot be in the past");
        if (endDate < startDate) throw new ValidationException("End date cannot be before start date");
        StartDate = startDate;
        EndDate = endDate;
        RaiseEventUpsertedDomainEvent();
    }

    public void UpdatePrice(Money price)
    {
        Price = price;
        RaiseEventUpsertedDomainEvent();
    }

    public void MarkAsSoldOut()
    {
        IsSoldOut = true;
        RaiseEventUpsertedDomainEvent();
    }
    
    private void RaiseEventUpsertedDomainEvent() => AddDomainEvent(new EventChanged(Id, EventName, StartDate, EndDate, VenueId, Price));
}