using Application.EventsManagement;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement;

[ApiController]
public class GetEventByIdEndpoint(GetEventById getEventById) : ControllerBase
{
    [HttpGet(Routes.TheEvent)]
    public async Task<ActionResult<Domain.EventsManagement.Event>> GetEvent(Guid id)
    {
        var @event = await getEventById.Execute(id);
        if (@event is null) return NotFound();
        return @event;
    }
}