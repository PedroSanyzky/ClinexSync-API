using System.Data;
using ClinexSync.Application.Data;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Administrators;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Offices;
using ClinexSync.Domain.Pacients;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IPublisher publisher
) : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    public DbSet<Person> Persons { get; private set; }
    public DbSet<Pacient> Pacients { get; private set; }
    public DbSet<Administrator> Administrators { get; private set; }
    public DbSet<Professional> Professionals { get; private set; }
    public DbSet<Office> Offices { get; private set; }
    public DbSet<Room> Rooms { get; private set; }
    public DbSet<City> Cities { get; private set; }
    public DbSet<District> Districts { get; private set; }
    public DbSet<Area> Areas { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}
