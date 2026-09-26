using Domain.EventsManagement.Venue;

namespace Application.EventsManagement.Venue;

public class GetVenueById(IPersistVenues venueRepository)
{
    public async Task<Domain.EventsManagement.Venue.Venue?> Execute(Guid id)
    {
        return await venueRepository.GetById(id);
    }
}