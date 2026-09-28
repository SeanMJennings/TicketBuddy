using Domain.Bookings.Venue;
using Infrastructure.Bookings.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Bookings.Venue;

public class VenueRepository(BookingDbContext bookingDbContext) : IPersistVenues
{
    public async Task Save(Domain.Bookings.Venue.Venue venue)
    {
        var existingVenue = await bookingDbContext.Venues
            .FirstOrDefaultAsync(v => v.Id == venue.Id);

        if (existingVenue is not null)
        {
            bookingDbContext.Update(venue);
            return;
        }

        bookingDbContext.Add(venue);
    }

    public async Task<Domain.Bookings.Venue.Venue?> GetById(Guid id)
    {
        return await bookingDbContext.Venues.FirstOrDefaultAsync(v => v.Id == id);
    }
}
