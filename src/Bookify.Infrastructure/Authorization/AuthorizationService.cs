using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Entities.Authorization;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        string cacheKey = $"auth:roles-{identityId}";
        UserRolesResponse? cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        UserRolesResponse roles = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }
 
    public async Task<UserRolePermissionResponse> GetUserRolePermissionAsync(string ctrlName,Guid userId)
    {
        string cacheKey = $"auth:user-permission-{ctrlName}-{userId}";
        UserRolePermissionResponse? cachedUserPermissions = await _cacheService.GetAsync<UserRolePermissionResponse>(cacheKey);

        if (cachedUserPermissions is not null)
        {
            return cachedUserPermissions;
        } 
     
        var userPermission = from yet in _dbContext.Set<Permission>()
                              join kyet in _dbContext.Set<UserPermission>() on yet.Id equals kyet.PermissionId
                              where yet.Name == ctrlName && kyet.UserId == userId
                              select new UserRolePermissionResponse
                              {
                                  Name = yet.Name,
                                  CanRead= kyet.Read,
                                  CanDelete= kyet.Delete,
                                  CanWrite= kyet.Write
                              };
        var rolePermission = from yet in _dbContext.Set<Permission>()
                        join rYet in _dbContext.Set<RolePermission>() on yet.Id equals rYet.PermissionId
                        join kRol in _dbContext.Set<UserRole>() on rYet.RoleId equals kRol.RoleId
                        where yet.Name == ctrlName && kRol.UserId == userId
                        select new UserRolePermissionResponse
                        {
                            Name = yet.Name,
                            CanRead = rYet.Read,
                            CanDelete= rYet.Delete,
                            CanWrite= rYet.Write
                        };
        
        
        var result = userPermission.Union(rolePermission).Distinct().FirstOrDefault() ?? new UserRolePermissionResponse();
        await _cacheService.SetAsync(cacheKey, result);

        return result;


    }
}
