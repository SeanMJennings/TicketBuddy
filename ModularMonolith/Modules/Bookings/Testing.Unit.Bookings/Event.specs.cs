using NUnit.Framework;

namespace Unit;

public partial class EventSpecs
{
    [Test]
    public void an_event_must_have_a_name()
    {
        Scenario(() =>
        {
            Given(valid_inputs);
            And(a_null_event_name);
            When(Validating(creating_an_event));
            Then(Informs("EventName cannot be null or empty"));
        });        
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(an_empty_event_name);
            When(Validating(creating_an_event));
            Then(Informs("EventName cannot be null or empty"));
        });
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(an_event_name_with_non_alphanumerical_characters);
            When(Validating(creating_an_event));
            Then(Informs("EventName can only have alphanumerical characters"));
        }); 
    }
    
    [Test]
    public void can_create_valid_event()
    {
        Given(valid_inputs);
        When(creating_an_event);
        Then(the_event_is_created);
    }
    
    [Test]
    public void can_update_event_name()
    {
        Given(a_valid_event);
        When(updating_event_name);
        Then(event_name_is_updated);
    }
    
    [Test]
    public void can_update_event_price()
    {
        Given(a_valid_event);
        When(updating_event_price);
        Then(event_price_is_updated);
    }
    
    [Test]
    public void can_update_event_venue()
    {
        Given(a_valid_event);
        When(updating_event_venue);
        Then(event_venue_is_updated);
    }
}
