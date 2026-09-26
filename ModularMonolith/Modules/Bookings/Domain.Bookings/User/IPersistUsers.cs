namespace Domain.Bookings.User;

public interface IPersistUsers
{
    public Task Upsert(User user);
}