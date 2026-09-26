using Domain.EventsManagement;

namespace Infrastructure.EventsManagement.Core;

public class UnitOfWork(EventManagementDbContext eventManagementDbContext) : IEventManagementUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken = default)
    {
        await eventManagementDbContext.Commit(cancellationToken);
    }
}