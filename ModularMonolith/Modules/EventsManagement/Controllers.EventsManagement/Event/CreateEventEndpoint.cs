﻿using Application.EventsManagement;
using Controllers.EventsManagement.Requests;
using Domain.ValueObjects;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EventsManagement;

[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class CreateEventEndpoint(CreateEvent createEvent) : ControllerBase
{
    [HttpPost(Routes.Events)]
    public async Task<CreatedResult> CreateEvent([FromBody] EventPayload payload)
    {
        var eventId = await createEvent.Execute(payload.EventName, payload.StartDate, payload.EndDate, payload.VenueId, new Money(payload.Price));
        return Created($"/{Routes.Events}/{eventId}", eventId);
    }
}