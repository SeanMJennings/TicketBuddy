using System.Security.Claims;
using Controllers.Bookings.Ticket;
using Domain.Exceptions;
using Infrastructure.Configuration;
using Infrastructure.Messaging;
using Infrastructure.Bookings.Configuration;
using Infrastructure.Bookings.Core.Configuration;
using MassTransit;
using MassTransit.Testing;
using Messages.EventsManagement;
using Messaging.Bookings.Consumers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Testing;
using Testing.Containers;

namespace Integration.Bookings;

public partial class GetTicketsForEventSpecs : TruncateDbSpecification
{
    private GetTicketsForEventEndpoint getTicketsForEventEndpoint = null!;
    private EventChangedConsumer _eventChangedConsumer = null!;
    private VenueChangedConsumer _venueChangedConsumer = null!;
    private ServiceProvider serviceProvider = null!;
    private ITestHarness testHarness = null!;

    private Guid event_id = Guid.CreateVersion7();
    private Guid user_id = Guid.CreateVersion7();
    private const decimal price = 25.00m;
    private const string name = "wibble";
    private readonly DateTime event_start_date = DateTime.Now.AddDays(1);
    private readonly DateTime event_end_date = DateTime.Now.AddDays(1).AddHours(2);

    protected override Task before_each()
    {
        event_id = Guid.CreateVersion7();
        user_id = Guid.CreateVersion7();

        serviceProvider = new ServiceCollection()
            .ConfigureInfrastructureServices()
            .ConfigureCache(Setup.Redis.GetConnectionString())
            .ConfigureBookingsDatabase(Setup.Database.GetConnectionString())
            .ConfigureSharedOutboxDatabase(Setup.Database.GetConnectionString())
            .AddMassTransitTestHarness(x =>
            {
                x.AddBookingsConsumers();
                x.AddSharedOutbox();
            })
            .ConfigureBookingsServices()
            .AddScoped<GetTicketsForEventEndpoint>()
            .BuildServiceProvider();

        testHarness = serviceProvider.GetRequiredService<ITestHarness>();
        testHarness.Start().GetAwaiter().GetResult();
        getTicketsForEventEndpoint = serviceProvider.GetRequiredService<GetTicketsForEventEndpoint>();
        AddUserClaimToControllerContext(user_id);
        _eventChangedConsumer = serviceProvider.GetRequiredService<EventChangedConsumer>();
        _venueChangedConsumer = serviceProvider.GetRequiredService<VenueChangedConsumer>();
        return Task.CompletedTask;
    }

    private void AddUserClaimToControllerContext(Guid userId)
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity([new Claim("sub", userId.ToString())], "TestAuth"));
        getTicketsForEventEndpoint.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    }

    protected override async Task after_each()
    {
        await Truncate(Setup.Database.GetConnectionString());
        await Setup.Redis.Clear();
        await testHarness.Stop();
    }

    private async Task an_event_exists()
    {
        var venueId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var venueContext = Substitute.For<ConsumeContext<VenueChanged>>();
        venueContext.Message.Returns(new VenueChanged
        {
            Id = venueId,
            Name = "Test Venue",
            Capacity = 17
        });
        await _venueChangedConsumer.Consume(venueContext);

        var eventContext = Substitute.For<ConsumeContext<EventChanged>>();
        eventContext.Message.Returns(new EventChanged
        {
            Id = event_id,
            EventName = name,
            StartDate = event_start_date,
            EndDate = event_end_date,
            VenueId = venueId,
            Price = price
        });
        await _eventChangedConsumer.Consume(eventContext);
    }

    private async Task requesting_the_tickets()
    {
        await getTicketsForEventEndpoint.GetTickets(event_id);
    }

    private async Task the_tickets_are_released()
    {
        var tickets = await getTicketsForEventEndpoint.GetTickets(event_id);
        tickets.Count.ShouldBe(17);
        tickets = tickets.OrderBy(t => t.SeatNumber).ToList();
        var counter = 1;
        foreach (var ticket in tickets)
        {
            ticket.EventId.ShouldBe(event_id);
            ticket.Price.ShouldBe(price);
            ticket.SeatNumber.ShouldBe(counter);
            counter++;
        }
    }

    private static void an_entity_not_found_exception_was_thrown() =>
        error.ShouldBeOfType<EntityNotFoundException>();
}