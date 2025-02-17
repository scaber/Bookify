using Bookify.Domain.Authorization;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories
{
    internal class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Guid> GetUserRoles(Guid userId)
        {
            return   DbContext.Set<UserRole>().Where(ur => ur.UserId == userId).SelectMany(x => x.Role.RolePermission).Select(x => x.PermissionId).ToList();
            
        }
    }
}
