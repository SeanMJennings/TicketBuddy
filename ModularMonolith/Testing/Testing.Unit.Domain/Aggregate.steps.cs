using Domain.Aggregates;

namespace Domain;

public partial class AggregateSpecs
{
    private class TestAggregate(Guid id) : Aggregate(id);
    private void creating_an_aggregate_with_empty_guid()
    {
        _ = new TestAggregate(Guid.Empty);
    }
}