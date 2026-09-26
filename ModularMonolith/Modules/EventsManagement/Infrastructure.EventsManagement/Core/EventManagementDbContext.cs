using Domain.ValueObjects;
using Infrastructure.Commands;
using Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventsManagement.Core;

public class EventManagementDbContext(
    DbContextOptions<EventManagementDbContext> options,
    DomainEventsDispatcher domainEventsDispatcher,
    IOutboxFlusher outboxFlusher)
    : UnitOfWorkDbContext<EventManagementDbContext>(options, domainEventsDispatcher, outboxFlusher)
{
    public DbSet<Domain.EventsManagement.Event> Events => Set<Domain.EventsManagement.Event>();
    public DbSet<Domain.EventsManagement.Venue.Venue> Venues => Set<Domain.EventsManagement.Venue.Venue>();
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.EventsManagement.Event>().HasKey(e => e.Id);
        modelBuilder.Entity<Domain.EventsManagement.Event>().Property(e => e.EventName).HasConversion(name => name.ToString(), name => new EventName(name));
        modelBuilder.Entity<Domain.EventsManagement.Event>().Property(e => e.Price).HasConversion(amount => (decimal)amount, amount => new Money(amount));
        modelBuilder.Entity<Domain.EventsManagement.Event>().Property(e => e.VenueId).HasColumnName("Venue");
        modelBuilder.Entity<Domain.EventsManagement.Event>().ToTable("Events","EventManagement", e => e.ExcludeFromMigrations());

        modelBuilder.Entity<Domain.EventsManagement.Venue.Venue>().HasKey(v => v.Id);
        modelBuilder.Entity<Domain.EventsManagement.Venue.Venue>().Property(v => v.Name).HasConversion(name => name.ToString(), name => new Domain.EventsManagement.Venue.VenueName(name));
        modelBuilder.Entity<Domain.EventsManagement.Venue.Venue>().ComplexProperty(v => v.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street).IsRequired();
            addressBuilder.Property(a => a.City).IsRequired();
            addressBuilder.Property(a => a.Postcode).IsRequired();
        });
        modelBuilder.Entity<Domain.EventsManagement.Venue.Venue>().ToTable("Venues", "EventManagement", e => e.ExcludeFromMigrations());
    }
}