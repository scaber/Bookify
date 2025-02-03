using Bookify.Domain.Authorization;
using Bookify.Domain.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class RolePermissionRepository : IRolePermissionRepository
{
    private readonly ApplicationDbContext _context;

    public RolePermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RolePermission>> GetAllAsync()
    {
        return await _context.Set<RolePermission>().ToListAsync();
    }

    public async Task<RolePermission> GetByIdAsync(Guid roleId, Guid permissionId)
    {
        return await _context.Set<RolePermission>()
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
    }

    public async Task AddAsync(RolePermission rolePermission)
    {
        await _context.Set<RolePermission>().AddAsync(rolePermission);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RolePermission rolePermission)
    {
        _context.Set<RolePermission>().Update(rolePermission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roleId, Guid permissionId)
    {
        var entity = await GetByIdAsync(roleId, permissionId);
        if (entity != null)
        {
            _context.Set<RolePermission>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
