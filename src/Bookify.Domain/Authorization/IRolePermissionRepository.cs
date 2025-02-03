using Bookify.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Authorization
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission>> GetAllAsync();
        Task<RolePermission> GetByIdAsync(Guid roleId, Guid permissionId);
        Task AddAsync(RolePermission rolePermission);
        Task UpdateAsync(RolePermission rolePermission);
        Task DeleteAsync(Guid roleId, Guid permissionId);
    }
}
