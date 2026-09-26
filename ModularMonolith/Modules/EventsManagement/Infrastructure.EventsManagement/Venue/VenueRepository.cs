using Application;
using Domain.EventsManagement.Venue;
using Infrastructure.EventsManagement.Core;
using Messages.EventsManagement;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventsManagement.Venue;

public class VenueRepository(EventManagementDbContext eventManagementDbContext, IPublishMessages publishEndpoint) : IPersistVenues
{
    public async Task Add(Domain.EventsManagement.Venue.Venue venue)
    {
        eventManagementDbContext.Add(venue);
        await publishEndpoint.Publish(new VenueUpserted
        {
            Id = venue.Id,
            Name = venue.Name,
            Capacity = venue.Capacity
        });
    }

    public async Task<Domain.EventsManagement.Venue.Venue?> GetById(Guid id)
    {
        return await eventManagementDbContext.Venues.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.EventsManagement.Venue.Venue>> GetAll()
    {
        return await eventManagementDbContext.Venues.ToListAsync();
    }

    public async Task Update(Domain.EventsManagement.Venue.Venue venue)
    {
        eventManagementDbContext.Update(venue);
        await publishEndpoint.Publish(new VenueUpserted
        {
            Id = venue.Id,
            Name = venue.Name,
            Capacity = venue.Capacity
        });
    }
}