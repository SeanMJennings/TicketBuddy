using Application.Bookings.Ticket.GetTicketsForEvent;
using Domain.Bookings.Ticket;
using Application;
using Application.Bookings.Ticket.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Bookings.Ticket;

[ApiController]
[Authorize(Roles = UserRoles.Customer)]
public class GetTicketsForEventEndpoint(GetTicketsForEvent getTicketsForEvent) : ControllerBase
{
    [HttpGet(Routes.Tickets)]
    public async Task<IList<TicketQuery>> GetTickets([FromRoute] Guid id)
    {
        return await getTicketsForEvent.Execute(id);
    }
}