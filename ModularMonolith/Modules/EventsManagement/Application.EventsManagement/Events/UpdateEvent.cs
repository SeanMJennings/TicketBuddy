using Domain.EventsManagement;
using Domain.ValueObjects;
using Messages.EventsManagement;

namespace Application.EventsManagement;

public class UpdateEvent(IPersistEvents eventRepository, IPublishMessages publishEndpoint, IEventManagementUnitOfWork unitOfWork)
{
    public async Task Execute(Guid eventId, EventName eventName, DateTimeOffset startDate, DateTimeOffset endDate, Money price)
    {
        var existingEvent = EventsValidator.CheckEventExists(await eventRepository.Get(eventId), eventId);
        EventsValidator.ValidateDate(startDate);
        existingEvent.UpdateName(eventName);
        existingEvent.UpdateDates(startDate, endDate);
        existingEvent.UpdatePrice(price);

        var allEvents = await eventRepository.GetAll();
        EventsValidator.CheckIfVenueAlreadyBooked(existingEvent, allEvents);
        eventRepository.Update(existingEvent);
        
        await publishEndpoint.Publish(new EventUpserted
        {
            Id = existingEvent.Id,
            EventName = existingEvent.EventName,
            StartDate = existingEvent.StartDate,
            EndDate = existingEvent.EndDate,
            VenueId = existingEvent.VenueId,
            Price = existingEvent.Price
        });
        
        await unitOfWork.Commit();
    }
}