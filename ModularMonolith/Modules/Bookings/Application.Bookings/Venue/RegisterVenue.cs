using Domain.Bookings.Core;
using Domain.Bookings.Venue;
using Messages.EventsManagement;

namespace Application.Bookings.Venue;

public class RegisterVenue(
    IPersistVenues venueRepository,
    IBookingUnitOfWork unitOfWork)
{
    public async Task Execute(VenueChanged message)
    {
        await venueRepository.Save(new Domain.Bookings.Venue.Venue(message.Id, message.Name, message.Capacity));
        await unitOfWork.Commit();
    }
}
