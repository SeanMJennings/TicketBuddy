using Domain.DomainEvents;
using Infrastructure.DomainEventsDispatching;
using NSubstitute;
using Shouldly;

namespace Infrastructure;

public partial class DomainEventsMapperSpecs
{
    private DomainEventsMapper mapper = null!;
    private IHandleDomainEvents? retrievedHandler;

    private void a_mapper_with_a_registered_event_handler()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(SampleHandler)).Returns(new SampleHandler());

        DomainEventsMapBuilder.Map<SampleEvent, SampleHandler>();
        var map = DomainEventsMapBuilder.Build();

        mapper = new DomainEventsMapper(map, serviceProvider);
    }

    private void a_mapper_with_handler_missing_from_service_provider()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(SampleHandler)).Returns(null);

        DomainEventsMapBuilder.Map<SampleEvent, SampleHandler>();
        var map = DomainEventsMapBuilder.Build();

        mapper = new DomainEventsMapper(map, serviceProvider);
    }

    private void an_empty_mapper()
    {
        mapper = new DomainEventsMapper(DomainEventsMapBuilder.Build(), Substitute.For<IServiceProvider>());
    }

    private void getting_handler_for_registered_event() => retrievedHandler = mapper.GetHandler(new SampleEvent());

    private void getting_handler_for_unregistered_event() => mapper.GetHandler(new AnotherSampleEvent());

    private void the_handler_is_returned() => retrievedHandler.ShouldNotBeNull();
    private static void a_key_not_found_exception_is_thrown() => error.ShouldBeOfType<KeyNotFoundException>();
    private static void an_invalid_operation_exception_is_thrown() => error.ShouldBeOfType<InvalidOperationException>();

    private record SampleEvent : IDescribeADomainEvent;
    private record AnotherSampleEvent : IDescribeADomainEvent;

    private class SampleHandler : HandleDomainEvents<SampleEvent>
    {
        protected override Task Handle(SampleEvent message) => Task.CompletedTask;
    }
}