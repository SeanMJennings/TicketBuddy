using Application.EventsManagement;
using Application.EventsManagement.Venue;
using Domain.EventsManagement;
using Domain.EventsManagement.Venue;
using Infrastructure.DomainEventsDispatching;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventsManagement.Core.Configuration;

public static class Services
{
    public static IServiceCollection ConfigureEventsManagementServices(this IServiceCollection services)
    {
        var eventHandlerMap = DomainEventsMapBuilder.Build();

        services
            .AddScoped<IEventManagementUnitOfWork, UnitOfWork>()
            .AddScoped<IPersistEvents, Event.EventRepository>()
            .AddScoped<CreateEvent>()
            .AddScoped<UpdateEvent>()
            .AddScoped<GetEvents>()
            .AddScoped<GetEventById>()
            .AddScoped<MarkEventAsSoldOut>()
            .AddScoped<IPersistVenues, Venue.VenueRepository>()
            .AddScoped<CreateVenue>()
            .AddScoped<UpdateVenue>()
            .AddScoped<GetVenueById>()
            .AddScoped<GetVenues>()
            .AddSingleton(eventHandlerMap);
        return services;
    }
}