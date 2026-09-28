using Application.Bookings.Venue;
using MassTransit;
using Messages.EventsManagement;

namespace Messaging.Bookings.Consumers
{
    public class VenueChangedConsumer(RegisterVenue registerVenue) : IConsumer<VenueChanged>
    {
        public async Task Consume(ConsumeContext<VenueChanged> context)
        {
            await registerVenue.Execute(context.Message);
        }
    }
}