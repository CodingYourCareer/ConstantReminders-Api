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
// File: ResponseDetail.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Models\ResponseDetail.cs
// ---------------------------------------------------------------------------
#endregion

namespace ConstantReminders.Contracts.Models;

public class ResponseDetail<TData>
{
    public ResultStatus? Status { get; set; }
    public required string Message { get; set; }
    public required string SubCode { get; set; }
    public TData? Data { get; set; }
    public List<ErrorDetail> ErrorDetails { get; set; } = [];
}