using Domain.DomainEvents;

namespace Domain.Aggregates;

public abstract class Aggregate
{
    protected Aggregate(Guid id)
    {
        Validation.BasedOn(errors =>
        {
            if (id == Guid.Empty)
            {
                errors.Add("Aggregate ID cannot be an empty GUID.");
            }
        });
        Id = id;
    }
    
    public Guid Id { get; }
    
    private readonly List<IDescribeADomainEvent> _domainEvents = [];

    [System.Text.Json.Serialization.JsonIgnore]
    public IReadOnlyCollection<IDescribeADomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void TransferDomainEventsFrom(Aggregate source)
    {
        foreach (var domainEvent in source.DomainEvents)
        {
            if (!_domainEvents.Contains(domainEvent))
            {
                _domainEvents.Add(domainEvent);
            }
        }
    }

    protected void AddDomainEvent(IDescribeADomainEvent domainEvent)
    {
        if (_domainEvents.Contains(domainEvent)) return;
        _domainEvents.Add(domainEvent);
    }
}