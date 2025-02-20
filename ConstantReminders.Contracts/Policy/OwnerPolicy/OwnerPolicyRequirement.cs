using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ConstantReminders.Contracts.Policy.OwnerPolicy;
//allows for injection of policy dependency
public class OwnerPolicyRequirement : IAuthorizationRequirement 
{
    public OwnerPolicyRequirement() { }
}
