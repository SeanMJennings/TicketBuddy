using Domain.EventsManagement;

namespace Application.EventsManagement;

public class GetEvents(IPersistEvents eventRepository)
{
    public async Task<IList<Event>> Execute()
    {
        return await eventRepository.GetAll();
    }
}