using Application.Bookings.Venue;
using MassTransit;
using VenueUpserted = Messages.EventsManagement.VenueUpserted;

namespace Messaging.Bookings.Consumers
{
    public class VenueUpsertedConsumer(UpsertVenue upsertVenue) : IConsumer<VenueUpserted>
    {
        public async Task Consume(ConsumeContext<VenueUpserted> context)
        {
            await upsertVenue.Execute(context.Message);
        }
    }
}