using Domain.Entities;

namespace Domain.Bookings.User
{
    public class User(Guid id, Name fullName, Email email) : Entity(id), IAmAnAggregateRoot
    {
        public Name FullName { get; private set; } = fullName;
        public Email Email { get; private set; } = email;

        public void UpdateName(Name newFullName) => FullName = newFullName;
        
        public void UpdateEmail(Email newEmail) => Email = newEmail;
    }
}
    