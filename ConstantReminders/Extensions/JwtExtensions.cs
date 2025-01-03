#region Copyright
// ---------------------------------------------------------------------------
// Copyright (c) 2025 Battleline Productions LLC. All rights reserved.
//
// Licensed under the Battleline Productions LLC license agreement.
// See LICENSE file in the project root for full license information.
//
// Author: Michael Cavanaugh
// Company: Battleline Productions LLC
// Date: 01/03/2025
// Solution Name: ConstantReminders-Api
// Project Name: ConstantReminders
// File: JwtExtensions.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders\Extensions\JwtExtensions.cs
// ---------------------------------------------------------------------------
#endregion

using System.IdentityModel.Tokens.Jwt;

namespace ConstantReminders.Extensions;

public static class JwtExtensions
{
    /// <summary>
    /// Retrieves the 'sub' claim from a given JWT bearer token string.
    /// </summary>
    /// <param name="bearerToken">A JWT bearer token (prefixed or not with "Bearer ").</param>
    /// <returns>The value of the 'sub' claim, or null if not found.</returns>
    public static string? GetJwtSub(this string bearerToken)
    {
        if (string.IsNullOrWhiteSpace(bearerToken))
            return null;

        // Remove "Bearer " prefix if it exists
        const string bearerPrefix = "Bearer ";
        if (bearerToken.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            bearerToken = bearerToken[bearerPrefix.Length..].Trim();
        }

        var handler = new JwtSecurityTokenHandler();

        var token = handler.ReadJwtToken(bearerToken);

        // Look for the 'sub' claim
        var subClaim = token.Claims.FirstOrDefault(c => c.Type == "sub");
        return subClaim?.Value;
    }
}