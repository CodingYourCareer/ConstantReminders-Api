#region Copyright
// ---------------------------------------------------------------------------
// Copyright (c) 2025 Battleline Productions LLC. All rights reserved.
//
// Licensed under the Battleline Productions LLC license agreement.
// See LICENSE file in the project root for full license information.
//
// Author: Michael Cavanaugh
// Company: Battleline Productions LLC
// Date: 01/03/2025
// Solution Name: ConstantReminders-Api
// Project Name: ConstantReminders.Api.Tests
// File: BaseRepositoryTests.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Api.Tests\Data\BaseRepositoryTests.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Api.Tests.Utilities;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Data;

namespace ConstantReminders.Api.Tests.Data;

public class BaseRepositoryTests
{
    [Fact]
    public async Task CreateAsync_ShouldInsertEventIntoDatabase()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Test Event",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser",
            CreatedDateTime = DateTime.UtcNow,
            UpdatedDateTime = DateTime.UtcNow
        };

        // Act
        var created = await repository.CreateAsync(@event);

        // Assert
        Assert.NotNull(created);
        Assert.Equal(@event.Id, created.Id);
        Assert.Equal("Test Event", created.Name);

        // Verify it actually exists in the DbContext
        var fromDb = await context.Events.FindAsync(@event.Id);
        Assert.NotNull(fromDb);
        Assert.Equal("Test Event", fromDb.Name);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfEventExists()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var eventId = Guid.NewGuid();
        var @event = new Event
        {
            Id = eventId,
            Name = "ExistsAsync Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };
        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Act
        var exists = await repository.ExistsAsync(eventId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfEventDoesNotExist()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        // Act
        var nonExistentId = Guid.NewGuid();
        var exists = await repository.ExistsAsync(nonExistentId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEvents()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var e1 = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Event One",
            CreatedBy = "User1",
            UpdatedBy = "User1"
        };
        var e2 = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Event Two",
            CreatedBy = "User2",
            UpdatedBy = "User2"
        };

        context.Events.AddRange(e1, e2);
        await context.SaveChangesAsync();

        // Act
        var events = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, events.Count);
        Assert.Contains(events, ev => ev.Name == "Event One");
        Assert.Contains(events, ev => ev.Name == "Event Two");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectEvent_IfExists()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var eventId = Guid.NewGuid();
        var @event = new Event
        {
            Id = eventId,
            Name = "GetById Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };

        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Act
        var fromRepo = await repository.GetByIdAsync(eventId);

        // Assert
        Assert.NotNull(fromRepo);
        Assert.Equal("GetById Test", fromRepo?.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfNotExists()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        // Act
        var fromRepo = await repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(fromRepo);
    }

    [Fact]
    public async Task UpdateAsync_ShouldPersistChanges()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Update Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };
        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Act
        @event.Name = "Updated Name";
        var updated = await repository.UpdateAsync(@event);

        // Assert
        Assert.Equal("Updated Name", updated.Name);

        var fromDb = await context.Events.FindAsync(@event.Id);
        Assert.Equal("Updated Name", fromDb.Name);
    }

    [Fact]
    public async Task DeleteAsync_ById_RemovesRecord()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var e = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Delete Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };

        context.Events.Add(e);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(e.Id);

        // Assert
        var fromDb = await context.Events.FindAsync(e.Id);
        Assert.Null(fromDb);
    }

    [Fact]
    public async Task DeleteAsync_ByEntity_RemovesRecord()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var e = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Delete Test By Entity",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };
        context.Events.Add(e);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(e);

        // Assert
        var fromDb = await context.Events.FindAsync(e.Id);
        Assert.Null(fromDb);
    }

    [Fact]
    public async Task ExecuteInTransactionAsync_ShouldCommitChanges_IfNoException()
    {
        // Use the SQLite-based in-memory context
        var context = TestDbContext.GetUcDashboardDbContextSqlite();
        var repository = new BaseRepository<Event>(context);

        var e = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Transaction Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };

        await repository.ExecuteInTransactionAsync(async () =>
        {
            context.Events.Add(e);
            await context.SaveChangesAsync();
            return true;
        });

        var fromDb = await context.Events.FindAsync(e.Id);
        Assert.NotNull(fromDb);
        Assert.Equal("Transaction Test", fromDb.Name);
    }

    [Fact]
    public async Task ExecuteInTransactionAsync_ShouldRollback_IfExceptionThrown()
    {
        // Arrange
        var context = TestDbContext.GetUcDashboardDbContextInMemory();
        var repository = new BaseRepository<Event>(context);

        var e = new Event
        {
            Id = Guid.NewGuid(),
            Name = "Transaction Rollback Test",
            CreatedBy = "TestUser",
            UpdatedBy = "TestUser"
        };

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await repository.ExecuteInTransactionAsync<Event>(async () =>
            {
                context.Events.Add(e);
                await context.SaveChangesAsync();

                // Simulate an error
                throw new InvalidOperationException("Test exception");
            });
        });

        // Assert
        // Because we threw an exception, the changes should be rolled back
        var fromDb = await context.Events.FindAsync(e.Id);
        Assert.Null(fromDb);
    }
}