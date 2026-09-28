namespace Domain.Bookings.Event;

public interface IPersistEvents
{
    public Task<Event?> GetById(Guid id);
    public Task Save(Event theEvent);
}