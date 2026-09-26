using Domain.EventsManagement;
using Domain.EventsManagement.Venue;

namespace Application.EventsManagement.Venue;

public class CreateVenue(IPersistVenues venueRepository, IEventManagementUnitOfWork unitOfWork)
{
    public async Task<Guid> Execute(VenueName name, Address address, uint capacity)
    {
        var venueId = Guid.CreateVersion7();
        var venue = new Domain.EventsManagement.Venue.Venue(venueId, name, address, capacity);

        var allVenues = await venueRepository.GetAll();
        VenuesValidator.CheckAddressUniqueness(address, allVenues);
        await venueRepository.Add(venue);
        await unitOfWork.Commit();
        return venueId;
    }
}