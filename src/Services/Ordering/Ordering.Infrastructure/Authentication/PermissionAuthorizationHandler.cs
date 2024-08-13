using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ordering.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequiment>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequiment requirement)
        {
            string? customerId = context.User.Claims
                                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(customerId, out Guid parseCustomerId)) 
            {
                return;
            }

            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();

            IPermissionService permissionService = serviceScope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permission = await permissionService.GetPermissionsAsync(parseCustomerId);

            if (permission.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
