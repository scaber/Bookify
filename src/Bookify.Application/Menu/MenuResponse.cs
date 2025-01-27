using Bookify.Domain.Menu;

namespace Bookify.Application.Menu;

public sealed class MenuResponse
{
    public string UniqueKey { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public int AuthorityId { get; set; }
    public string Url { get; set; }
    public int? TopMenuId { get; set; }
    public string AuthorityName { get; set; }
    public MenuResponse TopMenu { get; set; }
    public ICollection<MenuResponse> SubMenus { get; set; }
    public int? Order { get; set; }
    public byte Tip { get; set; }
}
