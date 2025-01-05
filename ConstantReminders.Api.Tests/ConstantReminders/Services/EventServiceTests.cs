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
// File: EventServiceTests.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Api.Tests\ConstantReminders\Services\EventServiceTests.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Services;
using NSubstitute;

namespace ConstantReminders.Api.Tests.ConstantReminders.Services;

public class EventServiceTests
{
    [Fact]
    public async Task CreateEvent_ShouldCreateAndReturnEventWithCreated201()
    {
        // Arrange
        var mockRepo = Substitute.For<IBaseRepository<Event>>();
        var service = new EventService(mockRepo);

        const string inputName = "TestEventName";
        const string inputUserId = "test-user-id";

        mockRepo.CreateAsync(Arg.Any<Event> ())
                .Returns(callInfo => callInfo.Arg<Event>());

        // Act
        var response = await service.CreateEvent(inputName, inputUserId);

        // Assert
        await mockRepo.Received(1).CreateAsync(Arg.Any<Event>());

        await mockRepo.Received(1).CreateAsync(Arg.Is<Event>(ev =>
            ev.Name == inputName &&
            ev.CreatedBy == inputUserId &&
            ev.UpdatedBy == inputUserId &&
            ev.Id != Guid.Empty
        ));

        Assert.Equal(ResultStatus.Created201, response.Status);
        Assert.Equal("Event Created!", response.Message);
        Assert.Equal("EventService", response.SubCode);
        Assert.NotNull(response.Data);
        Assert.Equal(inputName, response.Data.Name);
        Assert.Equal(inputUserId, response.Data.CreatedBy);
        Assert.Equal(inputUserId, response.Data.UpdatedBy);
        Assert.NotEqual(Guid.Empty, response.Data.Id);
    }
}