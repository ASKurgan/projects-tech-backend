using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Authorization
{
    public class SecretKeyRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAttribute permission)
        {
            if (context.User.HasClaim(c => c is { Type: "IsService", Value: "true" }))
            {
                context.Succeed(permission);
            }

            return Task.CompletedTask;
        }
    }
}
