using ASKTech.Framework.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.IntegrationTests.Auth
{
    public class TestRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAttribute permission)
        {
            context.Succeed(permission);
            return Task.CompletedTask;
        }
    }
}
