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
// Project Name: ConstantReminders.Contracts
// File: IEventService.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Interfaces\IEventService.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Contracts.Interfaces.Business;

public interface IEventService
{
    Task<ResponseDetail<Event>> CreateEvent(string name, string createdById);
    Task<ResponseDetail<List<Event>>> ListEvents();
}