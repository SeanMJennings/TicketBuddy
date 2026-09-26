using System.ComponentModel.DataAnnotations;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Bookings.Ticket;

public class Ticket(Guid id, Guid eventId, Money price, uint seatNumber) : Aggregate(id)
{
    public Guid EventId { get; private set; } = eventId;
    public Money Price { get; private set; } = price;
    public uint SeatNumber { get; private set; } = seatNumber;
    public Guid? UserId { get; private set; }
    public DateTimeOffset? PurchasedAt { get; private set; }
    
    public bool IsAvailable => UserId is null;
    
    public void Purchase(Guid userId)
    {
        if (UserId is not null) throw new ValidationException("Bookings are not available");
        UserId = userId;
        PurchasedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new TicketWasPurchased(Id, userId, EventId));
    }
    
    public void UpdatePrice(Money newPrice)
    {
        if (!IsAvailable) return;
        Price = newPrice;
    }
}