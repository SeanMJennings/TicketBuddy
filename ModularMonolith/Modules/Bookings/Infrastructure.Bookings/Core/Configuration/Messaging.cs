using Infrastructure.Bookings.Core;
using MassTransit;
using Messages.EventsManagement;
using Messaging.Bookings;
using Messaging.Bookings.Consumers;

namespace Infrastructure.Bookings.Configuration;

public static class Messaging
{
    public static void AddBookingsConsumers(this IBusRegistrationConfigurator x)
    {
        var bookingsIntegrationMessagingAssembly = BookingsMessaging.Assembly;
        x.AddConsumers(bookingsIntegrationMessagingAssembly);
        x.AddConsumer<UserRegisteredConsumer, UserRegisteredConsumerDefinition>();
    }

    public static void ConfigureBookingsMessaging(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.ReceiveEndpoint("bookings-queue", e =>
        {
            e.Bind<EventChanged>();
            e.Bind<VenueChanged>();
        });
    }
}