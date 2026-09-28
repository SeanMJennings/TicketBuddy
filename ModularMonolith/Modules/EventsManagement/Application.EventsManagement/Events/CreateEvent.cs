using Domain.EventsManagement;
using Domain.ValueObjects;
using Messages.EventsManagement;

namespace Application.EventsManagement;

public class CreateEvent(IPersistEvents eventRepository, IPublishMessages publishEndpoint, IEventManagementUnitOfWork unitOfWork)
{
    public async Task<Guid> Execute(EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Guid venueId, Money price)
    {
        var eventId = Guid.CreateVersion7();
        EventsValidator.ValidateDate(startDate);
        var theEvent = new Event(eventId, eventName, startDate, endDate, venueId, price);

        var allEvents = await eventRepository.GetAll();
        EventsValidator.CheckIfVenueAlreadyBooked(theEvent, allEvents);
        eventRepository.Add(theEvent);
        
        await publishEndpoint.Publish(new EventUpserted
        {
            Id = theEvent.Id,
            EventName = theEvent.EventName,
            StartDate = theEvent.StartDate,
            EndDate = theEvent.EndDate,
            VenueId = theEvent.VenueId,
            Price = theEvent.Price
        });
        
        await unitOfWork.Commit();
        return eventId;
    }
}