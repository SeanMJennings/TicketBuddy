using Application.EventsManagement.Venue;
using Controllers.EventsManagement.Requests;
using Domain.EventsManagement.Venue;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement.Venue;

[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class CreateVenueEndpoint(CreateVenue createVenue) : ControllerBase
{
    [HttpPost(Routes.Venues)]
    public async Task<CreatedResult> CreateVenue([FromBody] VenuePayload payload)
    {
        var address = new Address(payload.Street, payload.City, payload.Postcode);
        var venueId = await createVenue.Execute(payload.Name, address, payload.Capacity);
        return Created($"/{Routes.Venues}/{venueId}", venueId);
    }
}