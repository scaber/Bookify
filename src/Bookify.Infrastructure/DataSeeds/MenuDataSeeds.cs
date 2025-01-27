using Bookify.Domain.Entities.Authorization;
using Bookify.Domain.Menu;
using Bookify.Infrastructure;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class MenuDataSeeds
    {
        public static void Seed(ApplicationDbContext ctx)
        {
            var menuler = (MenuListesi);
            var yetkiler = ctx.Set<Permission>().ToList();

            var menuList = menuler.Select(m => SetMenu(m, yetkiler)).ToList();
            var menulerVeAltMenuler = menuList.Flatten(i => i.SubMenus).ToList();

            DeleteMenu(ctx, menulerVeAltMenuler);
            UpsertMenu(ctx, menulerVeAltMenuler);
        }

        private static Menu SetMenu(MenuSeedModel menu, List<Permission> permissions)
        {
            var menutoInsert = new Menu
            {
                UniqueKey = menu.UniqueKey,
                Title = menu.Title,
                Type = menu.Type,
                Url = menu.Url,
                Order = menu.Order,
                Icon = menu.Icon,
                PermissionName = menu.PermissionName,
                PermissionId = permissions.FirstOrDefault(x => x.Name == menu.PermissionName)?.Id ?? Guid.Empty,
                SubMenus = menu.SubMenus.Select(f => SetMenu(f, permissions)).ToList()
            };
            return menutoInsert;
        }

        public static readonly List<MenuSeedModel> MenuListesi = new List<MenuSeedModel>
    {

    new MenuSeedModel("menu-1", "home", "fas fa-home", "UI.Dashboard", "Menü üzerindeki > Ana Sayfa", 1, "/", 1),
     new MenuSeedModel("customer", "Customer.Singular", "fas fa-users", "UI.Customers", "Menü üzerindeki > Müşteriler", 2, null, 1,
        new MenuSeedModel("customer", "Customer.Plural", "", "UI.Customers.Customer", "Menü üzerindeki > Müşteri", 3, "/corporates", 1),
        new MenuSeedModel("supplier", "Supplier.Plural", "", "UI.Customers.Supplier", "Menü üzerindeki > Tedarikçi", 4, "/suppliers", 1)
    )
    };

        private static void DeleteMenu(ApplicationDbContext ctx, List<Menu> menulerVeAltMenuler)
        {
            ctx.Delete<Menu>(x => !menulerVeAltMenuler.Select(i => i.UniqueKey).Contains(x.UniqueKey));
            ctx.SaveChanges();
        }
        private static void UpsertMenu(ApplicationDbContext ctx, List<Menu> menuler)
        {
            ctx.Upsert(menuler, (x, y) => x.UniqueKey == y.UniqueKey);
            ctx.SaveChanges();
        }

        public static IEnumerable<T> Flatten<T>(
            this IEnumerable<T> items,
            Func<T, IEnumerable<T>> getChildren)
        {
            var stack = new Stack<T>();
            if (items != null)
                foreach (var item in items)
                    stack.Push(item);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;
                var children = getChildren(current);
                if (children == null) continue;

                foreach (var child in children)
                    stack.Push(child);
            }
        }
    }



    public class MenuSeedModel
    {
        public MenuSeedModel(
            string uniqueKey, string title, string icon, string authorityName, string authorityDescription, int order = 1, string url = null, byte tip = 1,
            params MenuSeedModel[] args)
        {
            UniqueKey = uniqueKey;
            Title = title;
            PermissionName = authorityName;
            PermissionDescription = authorityDescription;
            Order = order;
            Url = url;
            Type = tip;
            Icon = icon;
            SubMenus = args.ToList();
        }

        public string UniqueKey { get; set; }
        public string Title { get; set; }
        public int? Order { get; set; }
        public byte Type { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public List<MenuSeedModel> SubMenus { get; set; } = new List<MenuSeedModel>();
    }
}

