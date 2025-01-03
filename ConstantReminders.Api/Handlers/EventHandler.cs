#region Copyright
// ---------------------------------------------------------------------------
// Copyright (c) 2024 Battleline Productions LLC. All rights reserved.
//
// Licensed under the Battleline Productions LLC license agreement.
// See LICENSE file in the project root for full license information.
//
// Author: Michael Cavanaugh
// Company: Battleline Productions LLC
// Date: 12/12/2024
// Solution Name: ConstantReminders-Api
// Project Name: ConstantReminderApi
// File: EventHandler.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders-Api\Handlers\EventHandler.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminder.Api.Utility;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace ConstantReminder.Api.Handlers;

public static class EventHandler
{
    private const string BaseRoute = "api/v{version:apiVersion}/events";

    public static void MapAEventEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        app.MapPost($"{BaseRoute}/create", CreateEvent)
            .Produces<ResponseDetail<(Event?, string?)>>(StatusCodes.Status201Created)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Generate a new Event Item",
                Description = "Generates a new Event for your calendar."
            });
    }

    public static async Task<IResult> CreateEvent(
        [FromServices] IEventService eventService,
        [FromBody] CreateEventDto newEvent,
        HttpContext httpContext
    )
    {
        // Retrieve the user's "sub" claim (e.g. Auth0 user ID) from the HttpContext.
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        // If the userId claim doesn't exist (or is empty), return an appropriate status code.
        // Typically, missing authentication credentials is a "401 Unauthorized".
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Results.Unauthorized();
        }

        // Call your service logic to create the event, associating it with the current user.
        var result = await eventService.CreateEvent(newEvent.Name, userId);

        // Convert service result to a proper HTTP response (e.g., 200/201) using your ApiResponse helper.
        return ApiResponse.GetActionResult(result);
    }
}

public record CreateEventDto(string Name);