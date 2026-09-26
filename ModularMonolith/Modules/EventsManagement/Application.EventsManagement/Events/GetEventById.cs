using Domain.EventsManagement;

namespace Application.EventsManagement;

public class GetEventById(IPersistEvents eventRepository)
{
    public async Task<Event?> Execute(Guid eventId)
    {
        return await eventRepository.Get(eventId);
    }
}