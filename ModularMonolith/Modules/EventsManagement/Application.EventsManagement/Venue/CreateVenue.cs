using Domain.EventsManagement;
using Domain.EventsManagement.Venue;
using VenueAggregate = Domain.EventsManagement.Venue.Venue;

namespace Application.EventsManagement.Venue;

public class CreateVenue(IPersistVenues venueRepository, IEventManagementUnitOfWork unitOfWork)
{
    public async Task<Guid> Execute(VenueName name, Address address, uint capacity)
    {
        var venueId = Guid.CreateVersion7();
        var venue = VenueAggregate.Create(venueId, name, address, capacity);

        var allVenues = await venueRepository.GetAll();
        VenuesValidator.CheckAddressUniqueness(address, allVenues);
        venueRepository.Add(venue);
        await unitOfWork.Commit();
        return venueId;
    }
}