using Application.Bookings.Ticket.GetTicketsForUser;
using Domain.Bookings.Ticket;
using Application;
using Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Bookings.Ticket;

[ApiController]
[Authorize(Roles = UserRoles.Customer)]
public class GetTicketsForUserEndpoint(GetTicketsForUser getTicketsForUser) : ControllerBase
{
    [HttpGet(Routes.TicketsPurchased)]
    public async Task<IList<TicketQuery>> GetTicketsForUser()
    {
        var userId = User.GetUserId();
        return await getTicketsForUser.Execute(userId);
    }
}