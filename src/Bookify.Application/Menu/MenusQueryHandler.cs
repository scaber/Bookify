using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Authorization;
using Bookify.Domain.Menu;
using Bookify.Domain.Users;
using System.Data;

namespace Bookify.Application.Menus;

internal sealed class MenusQueryHandler
    : IQueryHandler<MenusQuery, IReadOnlyList<MenuResponse>>
{
    private readonly IUserContext _userContext;
    private readonly IMenuRepository _menuRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;

    

    public MenusQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext,
        IMenuRepository menuRepository,
        IUserRoleRepository userRoleRepository,
        IUserPermissionRepository userPermissionRepository,
        IRolePermissionRepository rolePermissionRepository)
    {
        _userContext = userContext;
        _menuRepository = menuRepository;
        _userRoleRepository = userRoleRepository;
        _userPermissionRepository = userPermissionRepository;
    }

    public Task<Result<IReadOnlyList<MenuResponse>>> Handle(MenusQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var userPermissions = _userPermissionRepository.GetByUserId(userId);
        var allPermissions = _userRoleRepository.GetUserRoles(userId).Union(userPermissions);

        var menus = _menuRepository.GetByMenuWithPermissionIds(allPermissions);
 

        var menuResponses = menus.Where(x=>x.TopMenuId is null).Select(menu => new MenuResponse
        {
            UniqueKey = menu.UniqueKey,
            Title = menu.Title,
            Icon = menu.Icon,
            PermissionId = menu.PermissionId,
            Url = menu.Url ?? string.Empty,
            TopMenuId = menu.TopMenuId,
            PermissionName = menu.PermissionName,
            Order = menu.Order,
            Tip = menu.Type,
            SubMenus = MapToMenuResponseList(menu.SubMenus) 
        }).ToList();
        return Task.FromResult<Result<IReadOnlyList<MenuResponse>>>(menuResponses);

    }  
    private static ICollection<MenuResponse> MapToMenuResponseList(ICollection<Menu> subMenus)
    {
        return subMenus?.Select(menu => new MenuResponse
        {
            UniqueKey = menu.UniqueKey,
            Title = menu.Title,
            Icon = menu.Icon,
            PermissionId = menu.PermissionId,
            Url = menu.Url,
            TopMenuId = menu.TopMenuId,
            PermissionName = menu.PermissionName,
            Order = menu.Order,
            Tip = menu.Type,
            SubMenus = MapToMenuResponseList(menu.SubMenus)
        }).ToList() ?? new List<MenuResponse>();
    }
            
}
