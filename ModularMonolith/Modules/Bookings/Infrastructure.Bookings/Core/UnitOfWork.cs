using Domain.Bookings.Core;

namespace Infrastructure.Bookings.Core;

public class UnitOfWork(BookingDbContext bookingDbContext) : IBookingUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken = default)
    {
        await bookingDbContext.Commit(cancellationToken);
    }
}

