using Microsoft.AspNetCore.Authorization;

namespace AuthTestProject.Authorization
{
    public class PermissionsRequirementsHandler : AuthorizationHandler<PermissionsRequirements>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionsRequirements requirement)
        {
            var hasPermission = context.User.Claims.Any(c => 
                c.Type == "permission" &&
                c.Value == requirement.Permission);

            if (hasPermission)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}