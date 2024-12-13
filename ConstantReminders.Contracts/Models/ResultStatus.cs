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
// File: ResultStatus.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Models\ResultStatus.cs
// ---------------------------------------------------------------------------
#endregion

namespace ConstantReminders.Contracts.Models;

public enum ResultStatus
{
    // 2xx Success
    Ok200 = 200,
    Created201 = 201,
    Accepted202 = 202,
    NoContent204 = 204,
    PartialContent206 = 206,

    // 3xx Redirection
    MovedPermanently301 = 301,
    Found302 = 302,
    SeeOther303 = 303,
    NotModified304 = 304,

    // 4xx Client Errors
    BadRequest400 = 400,
    NotAuthorized401 = 401,
    Forbidden403 = 403,
    NotFound404 = 404,
    MethodNotAllowed405 = 405,
    Conflict409 = 409,
    FailedDependency424 = 424,
    TooManyRequests429 = 429,

    // 5xx Server Errors
    Fatal500 = 500,
    NotImplemented501 = 501,
    BadGateway502 = 502,
    ServiceUnavailable503 = 503,
    GatewayTimeout504 = 504
}