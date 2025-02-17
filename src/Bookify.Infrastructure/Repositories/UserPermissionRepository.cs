using Bookify.Domain.Authorization;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories;

internal class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
{
    public UserPermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public List<Guid> GetByUserId(Guid userId)
    { 
        return (List<Guid>)DbContext.Set<UserPermission>().Where(up => up.UserId == userId).Select(x=>x.PermissionId).ToList();
    }
}
