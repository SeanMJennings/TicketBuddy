using MassTransit;
using Messaging.EventsManagement;

namespace Infrastructure.EventsManagement.Core.Configuration;

public static class Messaging
{
    public static void AddEventsManagementConsumers(this IBusRegistrationConfigurator x)
    {
        var eventsManagementIntegrationMessagingAssembly = EventsManagementMessaging.Assembly;
        x.AddConsumers(eventsManagementIntegrationMessagingAssembly);
    }

    public static void ConfigureEventsManagementMessaging(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.ReceiveEndpoint("events-management-queue", e =>
        {
            e.Bind<Messages.Bookings.EventSoldOut>();
        });
    }
}