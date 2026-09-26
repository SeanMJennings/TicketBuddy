using Domain.EventsManagement.Venue;

namespace Application.EventsManagement.Venue;

public class GetVenues(IPersistVenues venueRepository)
{
    public async Task<IEnumerable<Domain.EventsManagement.Venue.Venue>> Execute()
    {
        return await venueRepository.GetAll();
    }
}