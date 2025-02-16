using Bookify.Domain.Users;

namespace Bookify.Domain.Authorization;
public interface IRoleRepository
{
    void Add(Role role);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

}
