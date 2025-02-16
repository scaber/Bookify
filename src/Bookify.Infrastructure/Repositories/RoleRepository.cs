using Bookify.Domain.Authorization;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class RoleRepository : Repository<Role>, IRoleRepository
{
    protected readonly ApplicationDbContext DbContext;
    public RoleRepository(ApplicationDbContext dbContext)
       : base(dbContext)
    {
        DbContext= dbContext;
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext
             .Set<Role>()
             .FirstOrDefaultAsync(role => role.Name == name, cancellationToken);
    }  
}
