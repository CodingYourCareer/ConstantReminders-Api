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
// File: EventEndpointsTests.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Api.Tests\Api\Handlers\EventEndpointsTests.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminder.Api.Handlers;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using System.Security.Claims;
using EventHandler = ConstantReminder.Api.Handlers.EventHandler;

namespace ConstantReminders.Api.Tests.Api.Handlers;

public class EventEndpointsTests
{
    private readonly IEventService _mockEventService = Substitute.For<IEventService>();

    [Fact]
    public async Task CreateEvent_ReturnsCorrectResult_WhenSubClaimExists()
    {
        // Arrange
        // 1. Set up HttpContext with a 'sub' claim
        const string userId = "TestUserId";

        var claims = new List<Claim>
        {
            new("sub", userId)
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var user = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = user
        };

        // 2. Define the input DTO
        const string eventName = "My Test Event";

        var newEvent = new CreateEventDto(eventName);


        // 3. Define what the service should return when called
        //    Use a class/record/struct that matches your actual return type from CreateEvent
        var expectedServiceResult = new ResponseDetail<Event>
        {
            Status = ResultStatus.Ok200,
            Message = "Event Created",
            SubCode = "EventCreated",
            Data = new Event
            {
                CreatedBy = userId,
                UpdatedBy = userId,
                Name = eventName,
                Id = Guid.CreateVersion7(),
                 
            }
        };

        _mockEventService.CreateEvent(newEvent.Name, userId)
                         .Returns(expectedServiceResult);

        // Act
        // Call the static endpoint method
        var result = await EventHandler.CreateEvent(_mockEventService, newEvent, httpContext);

        // Assert
        // Here we verify a few things:
        // 1. The endpoint calls our service with correct parameters
        // 2. The final result matches our expectations
        await _mockEventService.Received(1).CreateEvent("My Test Event", "TestUserId");

        // You can assert that 'result' is the correct IResult subtype
        // or test its payload. In your examples, you often do something like:
        var okResult = Assert.IsType<Ok<ResponseDetail<Event>>>(result);
        Assert.Equal(expectedServiceResult, okResult.Value);
    }

    [Fact]
    public async Task CreateEvent_ReturnsUnauthorized_WhenSubClaimIsMissing()
    {
        // Arrange: Set up an HttpContext WITHOUT a 'sub' claim
        var identity = new ClaimsIdentity(authenticationType: "TestAuthType");
        var user = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = user
        };

        var newEvent = new CreateEventDto("My Event Without Sub");

        // We do NOT set up the mock service to return anything, because
        // the endpoint should short-circuit and return 401 before calling the service.

        // Act
        var result = await EventHandler.CreateEvent(_mockEventService, newEvent, httpContext);

        // Assert: verify the service is never called due to missing 'sub'
        await _mockEventService.DidNotReceive().CreateEvent(Arg.Any<string>(), Arg.Any<string>());

        // Since we return Results.Unauthorized(), the result will be a minimal-API "Unauthorized" IResult type.
        // If you're on .NET 6 or 7, the exact runtime type can differ (e.g. UnauthorizedHttpResult).
        // Use IsType to verify we got an Unauthorized result.
        Assert.IsType<UnauthorizedHttpResult>(result);
    }
}