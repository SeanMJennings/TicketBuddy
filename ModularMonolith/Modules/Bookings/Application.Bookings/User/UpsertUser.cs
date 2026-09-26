using Domain.Bookings.Core;
using Domain.Bookings.User;
using Messaging.Keycloak.Users;

namespace Application.Bookings.User;

public class UpsertUser(IPersistUsers userRepository, IBookingUnitOfWork unitOfWork)
{
    public async Task Execute(UserRegistered message)
    {
        var user = new Domain.Bookings.User.User(
            message.userId,
            new Name($"{message.details["first_name"]} {message.details["last_name"]}"),
            new Email(message.details["email"])
        );
        
        await userRepository.Upsert(user);
        await unitOfWork.Commit();
    }
}

