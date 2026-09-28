namespace Domain.Bookings.Venue;

public interface IPersistVenues
{
    Task Save(Venue venue);
    Task<Venue?> GetById(Guid id);
}