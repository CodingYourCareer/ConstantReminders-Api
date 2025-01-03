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
// File: ApiResponse.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders-Api\Utility\ApiResponse.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Models;

namespace ConstantReminderApi.Utility;

public static class ApiResponse
{
    public static IResult GetActionResult<T>(ResponseDetail<T?> response, string? resourceName = null)
    {
        var resourcePath = GetResourcePath<T>(resourceName);
        var resourceId = GetResourceId(response.Data);

        return HandleResult(response, resourcePath, resourceId);
    }

    public static IResult GetListActionResult<T>(ResponseDetail<List<T>> response, string? resourceName = null)
    {
        var resourcePath = GetResourcePath<T>(resourceName);
        var resourceId = response.Data is { Count: > 0 }
            ? GetResourceId(response.Data.First())
            : null;

        return HandleResult(response, resourcePath, resourceId);
    }

    private static string GetResourcePath<T>(string? resourceName)
    {
        return $"/{resourceName ?? typeof(T).Name.ToLower()}";
    }

    private static string? GetResourceId<T>(T? data)
    {
        return data switch
        {
            IEntity entity => entity.Id.ToString(),
            _ => null
        };
    }

    private static IResult HandleResult<T>(ResponseDetail<T> response, string resourcePath, string? resourceId)
    {
        return response.Status switch
        {
            // 2xx Success
            ResultStatus.Ok200 => TypedResults.Ok(response),
            ResultStatus.Created201 => TypedResults.Created($"{resourcePath}/{resourceId}", response),
            ResultStatus.Accepted202 => TypedResults.Accepted(resourcePath, response),
            ResultStatus.NoContent204 => TypedResults.NoContent(),
            ResultStatus.PartialContent206 => Results.Json(response, statusCode: 206),

            // 3xx Redirection
            ResultStatus.MovedPermanently301 => TypedResults.Redirect($"{resourcePath}/{resourceId}", permanent: true),
            ResultStatus.Found302 => TypedResults.Redirect($"{resourcePath}/{resourceId}", permanent: false),
            ResultStatus.SeeOther303 => TypedResults.Redirect($"{resourcePath}/{resourceId}", permanent: false),
            ResultStatus.NotModified304 => TypedResults.StatusCode(304),

            // 4xx Client Errors
            ResultStatus.BadRequest400 => TypedResults.BadRequest(response),
            ResultStatus.NotAuthorized401 => TypedResults.Unauthorized(),
            ResultStatus.Forbidden403 => TypedResults.StatusCode(403),
            ResultStatus.NotFound404 => TypedResults.NotFound(response),
            ResultStatus.MethodNotAllowed405 => TypedResults.StatusCode(405),
            ResultStatus.Conflict409 => TypedResults.Conflict(response),
            ResultStatus.FailedDependency424 => TypedResults.Problem(title: "Failed Dependency", statusCode: 424),
            ResultStatus.TooManyRequests429 => TypedResults.Problem(title: "Too Many Requests", statusCode: 429),

            // 5xx Server Errors
            ResultStatus.Fatal500 => TypedResults.Problem(title: "Internal Server Error", statusCode: 500),
            ResultStatus.NotImplemented501 => TypedResults.Problem(title: "Not Implemented", statusCode: 501),
            ResultStatus.BadGateway502 => TypedResults.Problem(title: "Bad Gateway", statusCode: 502),
            ResultStatus.ServiceUnavailable503 => TypedResults.Problem(title: "Service Unavailable", statusCode: 503),
            ResultStatus.GatewayTimeout504 => TypedResults.Problem(title: "Gateway Timeout", statusCode: 504),

            // Default
            _ => TypedResults.Problem(title: "An unknown error occurred", statusCode: 500)
        };
    }
}