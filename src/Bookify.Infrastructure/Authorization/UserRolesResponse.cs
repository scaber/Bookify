using Bookify.Domain.Authorization;
 
namespace Bookify.Infrastructure.Authorization;

internal sealed class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}
