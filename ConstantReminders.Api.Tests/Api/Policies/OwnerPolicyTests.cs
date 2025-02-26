using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ConstantReminder.Api.Policies;
using ConstantReminders.Contracts.Models;


namespace ConstantReminders.Api.Tests.Api.Policies;

public class OwnerPolicyTests
{
    [Fact]
    public async Task UserOwnsResource_Succeeds()
    {
        //Arrange
        var testHandler = new OwnerPolicyHandler();
        var testRequirement = new OwnerPolicyRequirement();
        var testEvent = new Event() { CreatedBy = "testCreated", Id = new Guid(), Name = "testName", UpdatedBy = "testUpdated" };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "testCreated")  // Different user
        }));

        var authContext = new AuthorizationHandlerContext(
            new[] { testRequirement }, claimsPrincipal, testEvent);

        //Act
        await testHandler.HandleAsync(authContext);

        //Assert
        Assert.True(authContext.HasSucceeded);
    }

    [Fact]
    public async Task UserDoesNotOwnResource_Fails()
    {
        var testHandler = new OwnerPolicyHandler();
        var testRequirement = new OwnerPolicyRequirement();
        var testEvent = new Event() { CreatedBy = "testCreated", Id = new Guid(), Name = "testName", UpdatedBy = "testUpdated" };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "testNotCreated")  // Different user
        }));

        var authContext = new AuthorizationHandlerContext(
            new[] { testRequirement }, claimsPrincipal, testEvent);

        await testHandler.HandleAsync(authContext);

        Assert.False(authContext.HasSucceeded);
    }

    [Fact]
    public async Task NoUser_Fails()
    {
        var testHandler = new OwnerPolicyHandler();
        var testRequirement = new OwnerPolicyRequirement();
        var testEvent = new Event() { CreatedBy = "testCreated", Id = new Guid(), Name = "testName", UpdatedBy = "testUpdated" };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        var authContext = new AuthorizationHandlerContext(
            new[] { testRequirement }, claimsPrincipal, testEvent);

        await testHandler.HandleAsync(authContext);

        Assert.False(authContext.HasSucceeded);
    }
}
