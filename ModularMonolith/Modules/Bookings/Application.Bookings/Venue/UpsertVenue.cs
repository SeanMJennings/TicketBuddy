using Domain.Bookings.Core;
using Domain.Bookings.Venue;
using VenueUpserted = Messages.EventsManagement.VenueUpserted;

namespace Application.Bookings.Venue;

public class UpsertVenue(
    IPersistVenues venueRepository,
    IBookingUnitOfWork unitOfWork)
{
    public async Task Execute(VenueUpserted message)
    {
        await venueRepository.Upsert(new Domain.Bookings.Venue.Venue(message.Id, message.Name, message.Capacity));
        await unitOfWork.Commit();
    }
}
