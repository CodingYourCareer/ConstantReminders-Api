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
// File: ErrorDetail.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Models\ErrorDetail.cs
// ---------------------------------------------------------------------------
#endregion

namespace ConstantReminders.Contracts.Models;

public class ErrorDetail
{
    public required string Instance { get; set; }
    public required string Detail { get; set; }
}