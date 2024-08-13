using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = Ordering.Infrastructure.Authentication.Permission;

namespace Ordering.Infrastructure.Data.Configurations
{
    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(
            Create(Role.Administrator, Permission.AllowAccessEndpoint));
        }

        public static RolePermission Create(Role role, Permission permission) 
        {
            return new RolePermission
            {
                RoleId = role.Id,
                PermissionId = (int)permission
            };
        }
    }
}
