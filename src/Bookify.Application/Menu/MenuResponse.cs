using Bookify.Domain.Menu;

namespace Bookify.Application.Menus;

public sealed class MenuResponse
{
    public string UniqueKey { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public Guid PermissionId { get; set; }
    public string Url { get; set; }
    public Guid? TopMenuId { get; set; }
    public string PermissionName { get; set; }
    public MenuResponse TopMenu { get; set; }
    public ICollection<MenuResponse> SubMenus { get; set; }
    public int? Order { get; set; }
    public byte Tip { get; set; }
}
