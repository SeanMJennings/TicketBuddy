using Domain.EventsManagement.Venue;
using Infrastructure.EventsManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventsManagement.Venue;

public class VenueRepository(EventManagementDbContext eventManagementDbContext) : IPersistVenues
{
    public void Add(Domain.EventsManagement.Venue.Venue venue)
    {
        eventManagementDbContext.Add(venue);
    }

    public async Task<Domain.EventsManagement.Venue.Venue?> GetById(Guid id)
    {
        return await eventManagementDbContext.Venues.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.EventsManagement.Venue.Venue>> GetAll()
    {
        return await eventManagementDbContext.Venues.ToListAsync();
    }

    public void Update(Domain.EventsManagement.Venue.Venue venue)
    {
        eventManagementDbContext.Update(venue);
    }
}