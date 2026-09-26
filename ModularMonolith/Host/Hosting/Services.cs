using Infrastructure.Configuration;
using Infrastructure.EventsManagement.Core.Configuration;
using Infrastructure.Notifications.Core.Configuration;
using Infrastructure.Bookings.Configuration;
using Infrastructure.Bookings.Core.Configuration;

namespace Api.Hosting;

public static class Services
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.ConfigureInfrastructureServices();
        services.ConfigureEventsManagementServices();
        services.ConfigureBookingsServices();
        services.ConfigureNotificationsServices();
    }
}