using Infrastructure.EventsManagement.Core.Configuration;
using Infrastructure.Messaging;
using Infrastructure.Notifications.Core.Configuration;
using Infrastructure.Bookings.Configuration;
using MassTransit;

namespace Api.Hosting;

internal static class Messaging
{
    internal static void ConfigureMessaging(this IServiceCollection services, string rabbitMqConnectionString)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddEventsManagementConsumers();
            x.AddBookingsConsumers();
            x.AddNotificationsConsumers();
            x.AddSharedOutbox();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConnectionString);
                cfg.ConfigureEventsManagementMessaging();
                cfg.ConfigureBookingsMessaging();
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}