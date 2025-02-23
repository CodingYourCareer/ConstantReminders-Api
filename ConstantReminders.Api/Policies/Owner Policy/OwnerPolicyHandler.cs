using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ConstantReminder.Api.Policies
{
    public class OwnerPolicyHandler : AuthorizationHandler<OwnerPolicyRequirement, User>
    {
        /*used to ensure only the user associated with a task can make changes to that task*/
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerPolicyRequirement requirement, User resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                         context.User.FindFirst("sub")?.Value;

            if (userId == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
