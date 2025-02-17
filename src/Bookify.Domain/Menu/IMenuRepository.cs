namespace Bookify.Domain.Menu;

public interface IMenuRepository
{
    Task<Menu?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IEnumerable<Menu> GetByMenuWithPermissionIds(IEnumerable<Guid> allPermissions);
}

