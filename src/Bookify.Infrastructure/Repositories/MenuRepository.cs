using Bookify.Domain.Menu;

namespace Bookify.Infrastructure.Repositories;

internal sealed class MenuRepository : Repository<Menu>, IMenuRepository
{
        public MenuRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    public   IEnumerable<Menu>  GetByMenuWithPermissionIds(IEnumerable<Guid> allPermissions)
    {
      return DbContext.Set<Menu>().Where(x => allPermissions.Contains(x.PermissionId)).Distinct(); 
    }
 
}
 
