using Application.EventsManagement.Venue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement.Venue;

[ApiController]
[AllowAnonymous]
public class GetVenuesEndpoint(GetVenues getVenues) : ControllerBase
{
    [HttpGet(Routes.Venues)]
    public async Task<IEnumerable<Domain.EventsManagement.Venue.Venue>> GetVenues()
    {
        return await getVenues.Execute();
    }
}