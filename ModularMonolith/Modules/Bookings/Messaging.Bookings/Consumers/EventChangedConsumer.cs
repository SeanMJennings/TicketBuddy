using Application.Bookings.Event;
using MassTransit;
using Messages.EventsManagement;

namespace Messaging.Bookings.Consumers
{
    public class EventChangedConsumer(RegisterEvent registerEvent) : IConsumer<EventChanged>
    {
        public async Task Consume(ConsumeContext<EventChanged> context)
        {
            await registerEvent.Execute(context.Message);
        }
    }
}