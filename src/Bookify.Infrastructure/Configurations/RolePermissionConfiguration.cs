using Bookify.Domain.Entities.Authorization;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

       builder.HasOne(rolePermission => rolePermission.Role)
            .WithMany(role => role.RolePermission)
            .HasForeignKey(rolePermission => rolePermission.RoleId);

        builder.HasOne(rolePermission => rolePermission.Permission)
            .WithMany(permission=> permission.RolePermissions)
            .HasForeignKey(rolePermission => rolePermission.PermissionId);
    }
}
