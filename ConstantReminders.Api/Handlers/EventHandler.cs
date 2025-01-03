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

using ConstantReminderApi.Utility;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace ConstantReminderApi.Handlers;

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

    private static async Task<IResult> CreateEvent([FromServices] IEventService eventService, [FromBody] CreateEventDto newEvent)
    {
        // TODO: How to get http context in minimal API
        //var userId from HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var result = await eventService.CreateEvent(newEvent.Name, "put auth0 user id here");

        return ApiResponse.GetActionResult(result);
    }
}

public record CreateEventDto(string Name);