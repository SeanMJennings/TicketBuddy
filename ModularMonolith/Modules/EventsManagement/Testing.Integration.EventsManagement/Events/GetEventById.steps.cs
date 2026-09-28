using Controllers.EventsManagement;
using Infrastructure.Configuration;
using Infrastructure.EventsManagement.Core.Configuration;
using Infrastructure.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Testing;
using Event = Domain.EventsManagement.Event;

namespace Integration.EventsManagement;

public partial class GetEventByIdSpecs : TruncateDbSpecification
{
    private GetEventByIdEndpoint getEventByIdEndpoint = null!;
    private ServiceProvider serviceProvider = null!;
    private ActionResult<Event>? getEventResult;

    protected override async Task before_each()
    {
        await base.before_each();
        getEventResult = null;

        serviceProvider = new ServiceCollection()
            .ConfigureInfrastructureServices()
            .ConfigureEventsManagementServices()
            .ConfigureEventsManagementDatabase(Setup.Database.GetConnectionString())
            .ConfigureSharedOutboxDatabase(Setup.Database.GetConnectionString())
            .AddMassTransitTestHarness(x =>
            {
                x.AddEventsManagementConsumers();
                x.AddSharedOutbox();
            })
            .AddScoped<GetEventByIdEndpoint>()
            .BuildServiceProvider();

        getEventByIdEndpoint = serviceProvider.GetRequiredService<GetEventByIdEndpoint>();
    }

    protected override async Task after_each()
    {
        await Truncate(Setup.Database.GetConnectionString());
    }

    private async Task requesting_a_non_existent_event()
    {
        getEventResult = await getEventByIdEndpoint.GetEvent(Guid.CreateVersion7());
    }

    private void a_not_found_response_is_returned()
    {
        getEventResult.ShouldNotBeNull();
        getEventResult.Result.ShouldBeOfType<NotFoundResult>();
    }
}