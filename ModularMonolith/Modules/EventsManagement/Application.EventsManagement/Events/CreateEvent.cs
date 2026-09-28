using Domain.EventsManagement;
using Domain.ValueObjects;

namespace Application.EventsManagement;

public class CreateEvent(IPersistEvents eventRepository, IEventManagementUnitOfWork unitOfWork)
{
    public async Task<Guid> Execute(EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Guid venueId, Money price)
    {
        var eventId = Guid.CreateVersion7();
        EventsValidator.ValidateDate(startDate);
        var theEvent = Event.Create(eventId, eventName, startDate, endDate, venueId, price);

        var allEvents = await eventRepository.GetAll();
        EventsValidator.CheckIfVenueAlreadyBooked(theEvent, allEvents);
        eventRepository.Add(theEvent);
        await unitOfWork.Commit();
        return eventId;
    }
}