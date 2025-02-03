
using Bookify.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user-roles");

         builder.HasKey(t => new { t.RoleId, t.UserId, t.Id });
        

        builder.HasOne(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        builder.HasOne(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);
    }
}
