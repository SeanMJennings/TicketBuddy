using Domain.EventsManagement.Venue;

namespace Controllers.EventsManagement.Requests;

public record UpdateVenuePayload(VenueName Name, string Street, string City, string Postcode, uint Capacity);
