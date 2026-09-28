using Domain.EventsManagement;
using Messages.Bookings;

namespace Application.EventsManagement;

public class MarkEventAsSoldOut(IPersistEvents eventRepository, IEventManagementUnitOfWork unitOfWork)
{
    public async Task Execute(EventSoldOut message)
    {
        var theEvent = await eventRepository.Get(message.EventId);
        if (theEvent is null) return;
        
        theEvent.MarkAsSoldOut();
        eventRepository.Update(theEvent);
        await unitOfWork.Commit();
    }
}