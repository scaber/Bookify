
namespace Bookify.Domain.Users;

public interface IUserPermissionRepository
{
    List<Guid> GetByUserId(Guid userId);
}
