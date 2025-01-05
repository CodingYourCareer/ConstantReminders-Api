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
// Project Name: ConstantReminders.Api.Tests
// File: JwtExtensionsTests.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Api.Tests\ConstantReminders\Extensions\JwtExtensionsTests.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConstantReminders.Api.Tests.ConstantReminders.Extensions;

public class JwtExtensionsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetJwtSub_ReturnsNull_WhenTokenIsNullOrWhitespace(string? token)
    {
        // Act
        var result = token.GetJwtSub();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetJwtSub_ReturnsSub_WithoutBearerPrefix()
    {
        // Arrange
        const string expectedSub = "my-user-id";
        var tokenString = GenerateTokenWithSub(expectedSub);

        // Act
        var result = tokenString.GetJwtSub();

        // Assert
        Assert.Equal(expectedSub, result);
    }

    [Fact]
    public void GetJwtSub_ReturnsSub_WithBearerPrefix()
    {
        // Arrange
        const string expectedSub = "my-user-id-bearer";
        var tokenString = GenerateTokenWithSub(expectedSub);
        var bearerString = "Bearer " + tokenString;

        // Act
        var result = bearerString.GetJwtSub();

        // Assert
        Assert.Equal(expectedSub, result);
    }

    [Fact]
    public void GetJwtSub_ReturnsNull_IfNoSubClaim()
    {
        // Arrange
        var token = new JwtSecurityToken(
            issuer: "test-issuer",
            audience: "test-audience",
            claims: [new Claim("name", "John Doe")],
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30)
        );
        var handler = new JwtSecurityTokenHandler();
        var tokenString = handler.WriteToken(token);

        // Act
        var result = tokenString.GetJwtSub();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetJwtSub_ThrowsException_ForInvalidToken()
    {
        // Arrange
        // A clearly invalid/malformed token
        const string invalidTokenString = "abc123.not-really-a-jwt";

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => invalidTokenString.GetJwtSub());
    }

    /// <summary>
    /// Helper method to generate a valid JWT token string with a given 'sub' claim.
    /// Uses minimal fields: issuer, audience, expiration, and a single 'sub' claim.
    /// </summary>
    private static string GenerateTokenWithSub(string subValue)
    {
        var token = new JwtSecurityToken(
            issuer: "test-issuer",
            audience: "test-audience",
            claims: [new Claim("sub", subValue)],
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30)
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}