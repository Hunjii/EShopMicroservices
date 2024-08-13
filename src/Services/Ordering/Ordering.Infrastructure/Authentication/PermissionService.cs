
using Ordering.Application.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PermissionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(Guid Id)
        {
            var roles = await _dbContext.Customers
                                    .Include(c => c.Roles)
                                    .ThenInclude(c => c.Permissions)
                                    .Where(c => c.Id == CustomerId.Of(Id))
                                    .Select(c => c.Roles)
                                    .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(r => r.Name)
                .ToHashSet();
        }
    }
}
