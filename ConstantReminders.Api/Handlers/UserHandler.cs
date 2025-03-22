using ConstantReminders.Contracts.DTO;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace ConstantReminder.Api.Handlers;

public static class UserHandler
{
    private const string BaseRoute = "api/v{version:apiVersion}/users";

    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        // Map endpoints for user CRUD operations
        app.MapPost($"{BaseRoute}/create", CreateUser)
            .Produces<ResponseDetail<(UserDto?, string?)>>(StatusCodes.Status201Created)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a new User",
                Description = "Creates a new user in the system."
            }).RequireAuthorization();

        app.MapGet($"{BaseRoute}", GetUsers)
            .Produces<ResponseDetail<(List<UserDto>?, string?)>>(StatusCodes.Status200OK)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all Users",
                Description = "Lists all users in the database."
            }).RequireAuthorization();

        app.MapGet($"{BaseRoute}/{{id}}", GetUserById)
            .Produces<ResponseDetail<(UserDto?, string?)>>(StatusCodes.Status200OK)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get User by Id",
                Description = "Retrieves a specific user by their unique ID."
            }).RequireAuthorization();

        app.MapPut($"{BaseRoute}/{{id}}", UpdateUser)
            .Produces<ResponseDetail<(UserDto?, string?)>>(StatusCodes.Status200OK)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update an existing User",
                Description = "Updates the details of an existing user."
            }).RequireAuthorization();

        app.MapDelete($"{BaseRoute}/{{id}}", DeleteUser)
            .Produces<ResponseDetail<(bool, string?)>>(StatusCodes.Status204NoContent)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a User",
                Description = "Deletes a user from the system."
            }).RequireAuthorization();
    }

    // Create a new user
    public static async Task<IResult> CreateUser(
        [FromServices] IUserService userService,
        [FromBody] CreateUpdateUserDto newUserDto,
        HttpContext httpContext
    )
    {
        var userId = httpContext.User.FindFirst("sub")?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Results.Unauthorized();
        }

        var result = await userService.CreateUser(newUserDto, "Admin", userId);

        return ApiResponse.GetActionResult(result);
    }

    // Get list of users
    public static async Task<IResult> GetUsers([FromServices] IUserService userService)
    {
        var result = await userService.GetUsers();
        return ApiResponse.GetActionResult(result);
    }

    // Get user by ID
    public static async Task<IResult> GetUserById(
        [FromServices] IUserService userService,
        Guid id
    )
    {
        var result = await userService.GetUserById(id);
        return ApiResponse.GetActionResult(result);
    }

    // Update user
    public static async Task<IResult> UpdateUser(
        [FromServices] IUserService userService,
        [FromBody] CreateUpdateUserDto updatedUserDto,
        Guid id,
        HttpContext httpContext
    )
    {
        var userId = httpContext.User.FindFirst("sub")?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Results.Unauthorized();
        }

        var result = await userService.UpdateUser(updatedUserDto, id, userId);
        return ApiResponse.GetActionResult(result);
    }

    // Delete user
    public static async Task<IResult> DeleteUser(
        [FromServices] IUserService userService,
        Guid id
    )
    {
        var result = await userService.DeleteUser(id);
        return ApiResponse.GetActionResult(result);
    }
}

