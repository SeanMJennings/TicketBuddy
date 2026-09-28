using Domain.Bookings.Core;
using Domain.Bookings.Event;
using EventChanged = Messages.EventsManagement.EventChanged;

namespace Application.Bookings.Event;

public class RegisterEvent(
    IPersistEvents eventRepository,
    IBookingUnitOfWork unitOfWork)
{
    public async Task Execute(EventChanged message)
    {
        await eventRepository.Save(Domain.Bookings.Event.Event.Create(message.Id, message.EventName, message.VenueId, message.Price));
        await unitOfWork.Commit();
    }
}