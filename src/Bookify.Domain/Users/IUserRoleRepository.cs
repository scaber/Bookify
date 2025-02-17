

namespace Bookify.Domain.Users
{
    public interface IUserRoleRepository
    {
        List<Guid> GetUserRoles(Guid userId);
    }
}
