using Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands;

public abstract class UnitOfWorkDbContext<T>(
    DbContextOptions<T> options,
    DomainEventsDispatcher domainEventsDispatcher,
    IOutboxFlusher outboxFlusher)
    : DbContext(options)
    where T : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    public Task Commit(CancellationToken cancellationToken = default) =>
        Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await SaveChangesAsync(cancellationToken);
                await domainEventsDispatcher.DispatchEvents(this);
                await SaveChangesAsync(cancellationToken);
                await outboxFlusher.FlushAsync(cancellationToken);
                ChangeTracker.Clear();
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
}