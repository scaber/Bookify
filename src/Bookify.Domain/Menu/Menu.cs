using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Menu
{
    public sealed class Menu : Entity
    {
        public string UniqueKey { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public Guid PermissionId { get; set; }
        public string? Url { get; set; }
        public Guid? TopMenuId { get; set; }
        public string PermissionName { get; set; }
        public  Menu TopMenu { get; set; }
        public ICollection<Menu> SubMenus { get; set; }
        public int? Order { get; set; }
        public byte Type { get; set; }
    }
}
