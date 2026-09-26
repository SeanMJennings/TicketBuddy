using Infrastructure.EventsManagement.Core.Configuration;
using Infrastructure.Messaging;
using Infrastructure.Notifications.Core.Configuration;
using Infrastructure.Bookings.Core.Configuration;

namespace Api.Hosting;

internal static class Database
{
    internal static void ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.ConfigureEventsManagementDatabase(connectionString);
        services.ConfigureBookingsDatabase(connectionString);
        services.ConfigureNotificationsDatabase(connectionString);
        services.ConfigureSharedOutboxDatabase(connectionString);
    }
}