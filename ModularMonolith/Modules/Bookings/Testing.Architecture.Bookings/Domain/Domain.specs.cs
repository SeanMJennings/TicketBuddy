namespace Testing.Architecture.Bookings.Domain;

internal partial class DomainSpecs
{
    [Test]
    public void domain_events_should_be_immutable()
    {
        Given(domain_event_types);
        Then(should_be_immutable);
    }
    
    [Test]
    public void domain_primitives_should_be_immutable()
    {
        Given(domain_primitives);
        Then(should_be_immutable);
    }

    [Test]
    public void aggregates_cannot_have_reference_to_other_aggregate()
    {
        Given(aggregates);
        Then(should_not_reference_other_aggregate);
    }
}