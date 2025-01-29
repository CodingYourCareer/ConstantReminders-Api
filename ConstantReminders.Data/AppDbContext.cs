using ConstantReminders.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstantReminders.Data;

// dotnet ef migrations add InitialMigration --project ../ConstantReminders.Data/ConstantReminders.Data.csproj  --context AppDbContext
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Event>()
            .HasOne(x => x.NotificationSchedule)
            .WithOne(x => x.Event)
            .HasForeignKey<NotificationSchedule>(y => y.EventId);
            

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is IEntity entity)
            {
                entity.UpdatedDateTime = now;

                if (entry.State == EntityState.Added)
                {
                    entity.Id = entity.Id == Guid.Empty ? Guid.CreateVersion7() : entity.Id;
                    entity.CreatedDateTime = now;
                }
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is IEntity entity)
            {
                entity.UpdatedDateTime = now;

                if (entry.State == EntityState.Added)
                {
                    entity.Id = entity.Id == Guid.Empty ? Guid.CreateVersion7() : entity.Id;
                    entity.CreatedDateTime = now;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}