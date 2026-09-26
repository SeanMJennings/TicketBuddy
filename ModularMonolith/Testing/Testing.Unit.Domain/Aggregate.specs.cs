using BDD;
using NUnit.Framework;

namespace Domain;

public partial class AggregateSpecs : Specification
{
    [Test]
    public void aggregate_should_not_allow_empty_guid_as_id()
    {
        When(Validating(creating_an_aggregate_with_empty_guid));
        Then(Informs("Aggregate ID cannot be an empty GUID."));
    }
}