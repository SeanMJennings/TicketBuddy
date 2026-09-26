using Application.Bookings.Event;
using MassTransit;
using EventUpserted = Messages.EventsManagement.EventUpserted;

namespace Messaging.Bookings.Consumers
{
    public class EventUpsertedConsumer(UpsertEvent upsertEvent) : IConsumer<EventUpserted>
    {
        public async Task Consume(ConsumeContext<EventUpserted> context)
        {
            await upsertEvent.Execute(context.Message);
        }
    }
}