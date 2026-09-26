using Application.EventsManagement;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement;

[ApiController]
public class GetEventsEndpoint(GetEvents getEvents) : ControllerBase
{
    [HttpGet(Routes.Events)]
    public async Task<IList<Domain.EventsManagement.Event>> GetEvents()
    {
        return await getEvents.Execute();
    }
}