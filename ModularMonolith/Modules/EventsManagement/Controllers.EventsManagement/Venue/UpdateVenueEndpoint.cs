using Application.EventsManagement.Venue;
using Controllers.EventsManagement.Requests;
using Domain.EventsManagement.Venue;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement.Venue;

[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class UpdateVenueEndpoint(UpdateVenue updateVenue) : ControllerBase
{
    [HttpPut(Routes.TheVenue)]
    public async Task<ActionResult> UpdateVenue(Guid id, [FromBody] UpdateVenuePayload payload)
    {
        await updateVenue.Execute(id, payload.Name, new Address(payload.Street, payload.City, payload.Postcode), payload.Capacity);
        return NoContent();
    }
}
