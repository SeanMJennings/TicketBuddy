namespace Messages.Bookings;

public record EventSoldOut
{
    public Guid EventId { get; init; }
}
