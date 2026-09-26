using Application.EventsManagement;
using MassTransit;
using Messages.Bookings;

namespace Messaging.EventsManagement
{
    public class EventSoldOutConsumer(MarkEventAsSoldOut markEventAsSoldOut) : IConsumer<EventSoldOut>
    {
        public async Task Consume(ConsumeContext<EventSoldOut> context)
        {
            await markEventAsSoldOut.Execute(context.Message);
        }
    }
}