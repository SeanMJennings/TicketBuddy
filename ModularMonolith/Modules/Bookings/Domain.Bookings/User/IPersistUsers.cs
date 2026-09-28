namespace Domain.Bookings.User;

public interface IPersistUsers
{
    public Task Save(User user);
}