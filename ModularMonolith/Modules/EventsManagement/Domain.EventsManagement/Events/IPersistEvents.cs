namespace Domain.EventsManagement;

public interface IPersistEvents
{
    public void Add(Event theEvent);
    public void Update(Event theEvent);
    public Task<Event?> Get(Guid id);
    public Task<IList<Event>> GetAll();
}