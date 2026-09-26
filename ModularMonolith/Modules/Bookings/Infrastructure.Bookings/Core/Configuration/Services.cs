using Application.Bookings.Event;
using Application.Bookings.Ticket;
using Application.Bookings.Ticket.GetTicketsForEvent;
using Application.Bookings.Ticket.GetTicketsForUser;
using Application.Bookings.Ticket.PurchaseTickets;
using Application.Bookings.Ticket.Queries;
using Application.Bookings.Ticket.ReserveTickets;
using Application.Bookings.User;
using Application.Bookings.Venue;
using Domain.Bookings.Core;
using Domain.Bookings.Event;
using Domain.Bookings.Ticket;
using Domain.Bookings.User;
using Domain.Bookings.Venue;
using Infrastructure.DomainEventsDispatching;
using Microsoft.Extensions.DependencyInjection;
using EventUpsertedHandler = Domain.Bookings.Event.EventUpsertedHandler;

namespace Infrastructure.Bookings.Core.Configuration;

public static class Services
{
    public static IServiceCollection ConfigureBookingsServices(this IServiceCollection services)
    {
        var eventHandlerMap = new DomainEventsMapBuilder()
            .Map<EventUpserted, EventUpsertedHandler>()
            .Map<TicketWasPurchased, TicketWasPurchasedHandler>()
            .Build();

        services
            .AddScoped<IBookingUnitOfWork, UnitOfWork>()
            .AddScoped<IPersistEvents, Event.EventRepository>()
            .AddScoped<UpsertEvent>()
            .AddScoped<EventUpsertedHandler>()
            .AddScoped<IPersistVenues, Venue.VenueRepository>()
            .AddScoped<UpsertVenue>()
            .AddScoped<IPersistTickets, Ticket.TicketRepository>()
            .AddScoped<IQueryTickets, Ticket.TicketQuerist>()
            .AddScoped<IQueryTicketReservations, Ticket.TicketReservationCacheRepository>()
            .AddScoped<IExtendTicketsInTheReservationCache, Ticket.TicketReservationCacheRepository>()
            .AddScoped<PurchaseTickets>()
            .AddScoped<ReserveTickets>()
            .AddScoped<GetTicketsForEvent>()
            .AddScoped<GetTicketsForUser>()
            .AddScoped<TicketWasPurchasedHandler>()
            .AddScoped<IPersistUsers, User.UserRepository>()
            .AddScoped<UpsertUser>()
            .AddSingleton(eventHandlerMap);
        return services;
    }
}