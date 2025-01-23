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
// File: Event.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Models\Event.cs
// ---------------------------------------------------------------------------
#endregion


namespace ConstantReminders.Contracts.Models;

public class Event : IEntity
{
    public virtual required Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
    public required NotificationSchedule NotificationSchedule { get; set; }
}