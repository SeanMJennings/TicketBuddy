using Domain.DomainEvents;

namespace Infrastructure.DomainEventsDispatching;

public class DomainEventsMapper(IReadOnlyDictionary<Type, Type> map, IServiceProvider serviceProvider)
{
    public IHandleDomainEvents GetHandler(IDescribeADomainEvent domainEvent)
    {
        if (map.TryGetValue(domainEvent.GetType(), out var handlerType))
        {
            return serviceProvider.GetService(handlerType) as IHandleDomainEvents
                   ?? throw new InvalidOperationException($"Handler of type {handlerType} not found in the service provider.");
        }
        throw new KeyNotFoundException($"No handler found for domain event of type {domainEvent.GetType()}");
    }
}

public static class DomainEventsMapBuilder
{
    private static readonly Dictionary<Type, Type> _map = [];

    public static void Map<TEvent, THandler>()
        where TEvent : IDescribeADomainEvent
        where THandler : HandleDomainEvents<TEvent>
    {
        _map[typeof(TEvent)] = typeof(THandler);
    }

    public static IReadOnlyDictionary<Type, Type> Build() => _map.AsReadOnly();
}