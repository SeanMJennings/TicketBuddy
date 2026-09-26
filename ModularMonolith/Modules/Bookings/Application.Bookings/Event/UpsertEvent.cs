using Domain.Bookings.Core;
using Domain.Bookings.Event;
using EventUpserted = Messages.EventsManagement.EventUpserted;

namespace Application.Bookings.Event;

public class UpsertEvent(
    IPersistEvents eventRepository,
    IBookingUnitOfWork unitOfWork)
{
    public async Task Execute(EventUpserted message)
    {
        await eventRepository.Upsert(Domain.Bookings.Event.Event.Create(message.Id, message.EventName,
            message.StartDate, message.EndDate, message.VenueId, message.Price));
        await unitOfWork.Commit();
    }
}