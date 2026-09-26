namespace Controllers.Bookings.Requests;

public record TicketPurchasePayload(Guid[] ticketIds);