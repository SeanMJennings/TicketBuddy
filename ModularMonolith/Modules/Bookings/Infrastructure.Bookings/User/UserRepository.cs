using Domain.Bookings.User;
using Infrastructure.Bookings.Core;

namespace Infrastructure.Bookings.User;

public class UserRepository(BookingDbContext bookingDbContext) : IPersistUsers
{
    public async Task Save(Domain.Bookings.User.User theUser)
    {
        var existingUser = await Get(theUser.Id);
        if (existingUser is not null)
        {
            existingUser.UpdateName(theUser.FullName);
            existingUser.UpdateEmail(theUser.Email);
            bookingDbContext.Update(existingUser);
        }
        else
        {
            bookingDbContext.Add(theUser);
        }
    }

    private async Task<Domain.Bookings.User.User?> Get(Guid id)
    {
        return await bookingDbContext.Users.FindAsync(id);
    }
}

