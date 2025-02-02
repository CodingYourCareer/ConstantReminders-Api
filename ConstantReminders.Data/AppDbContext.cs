using ConstantReminders.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace ConstantReminders.Data;

// dotnet ef migrations add InitialMigration --project ../ConstantReminders.Data/ConstantReminders.Data.csproj  --context AppDbContext
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>(); 
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        modelBuilder.ApplyConfiguration(new Configuration.EventConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.UserConfiguration());
        base.OnModelCreating(modelBuilder);
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