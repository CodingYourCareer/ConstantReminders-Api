using Microsoft.AspNetCore.Authorization;

namespace ConstantReminder.Api.Policies;

public class OwnerPolicyRequirement : IAuthorizationRequirement
{
    public OwnerPolicyRequirement() { }
}
