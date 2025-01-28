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
// Project Name: ConstantReminders
// File: EventService.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders\Services\EventService.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Services;

public class EventService(IBaseRepository<Event> eventRepo) : IEventService
{
    private const string SubCode = "EventService";

    public async Task<ResponseDetail<Event>> CreateEvent(string name, string createdById)
    {
        var createEvent = new Event
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            CreatedBy = createdById,
            UpdatedBy = createdById,
            
        };

        var result = await eventRepo.CreateAsync(createEvent);

        return new ResponseDetail<Event>
        {
            Data = result,
            Message = "Event Created!",
            SubCode = SubCode,
            Status = ResultStatus.Created201
        };
    }

    public async Task<ResponseDetail<List<Event>>> ListEvents()
    {
        var result = await eventRepo.List();

        return new ResponseDetail<List<Event>>
        {
            Data = result,
            Message = $"Found {result.Count} events",
            SubCode = SubCode,
            Status = ResultStatus.Ok200
        };
    }
}