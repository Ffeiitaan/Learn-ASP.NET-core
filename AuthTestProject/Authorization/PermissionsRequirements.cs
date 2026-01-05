using Microsoft.AspNetCore.Authorization;

namespace AuthTestProject.Authorization
{
    public class PermissionsRequirements : IAuthorizationRequirement
    {
        public string Permission { get; }
        public PermissionsRequirements(string permission)
        {
            Permission = permission;
        }
    }
}