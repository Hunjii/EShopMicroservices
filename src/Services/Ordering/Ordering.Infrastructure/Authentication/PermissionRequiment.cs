using Microsoft.AspNetCore.Authorization;

namespace Ordering.Infrastructure.Authentication
{
    public class PermissionRequiment : IAuthorizationRequirement
    {
        public PermissionRequiment(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
