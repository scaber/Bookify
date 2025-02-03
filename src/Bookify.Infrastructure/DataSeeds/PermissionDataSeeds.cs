using Bookify.Infrastructure;
using Bookify.Domain.Authorization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class PermissionDataSeeds
    {  
        public static void Seed(ApplicationDbContext applicationDbContext, Assembly assembly)
        {
            var menus = MenuDataSeeds.MenuLists;
            var menuTreeView = menus.Flatten(x => x.SubMenus).ToList();

            var permissions = PermissionList(menuTreeView, assembly);
            var permissionList = new List<Permission>(permissions);

            permissions.ForEach(permission =>
            {
                if (permission.Seviye != 1)
                {
                    var split = permission.Name.Split('.');
                    var parentName = string.Join(".", split.Take(split.Length - 1));
                    if (permissionList.All(x => x.Name != parentName))
                    {
                        permissionList.Add(new Permission()
                        {
                            Seviye = split.Length - 1,
                            Description = parentName,
                            Name = parentName
                        });
                    }
                }
            });

            DeletePermission(applicationDbContext, permissionList);
            UpsertPermission(applicationDbContext, permissionList);

        }
 
        private static void DeletePermission(ApplicationDbContext ctx, List<Permission> permissionList)
        {
            ctx.Delete<Permission>(x => !permissionList.Select(i => i.Name).Contains(x.Name));
            ctx.SaveChanges();
        }
 
        private static void UpsertPermission(ApplicationDbContext ctx, List<Permission> permissionList)
        {
            ctx.Upsert(permissionList, (x, y) => y.Name == x.Name);
            ctx.SaveChanges();
        }

        private static List<Permission> PermissionList(List<MenuSeedModel> menulerVeAltMenuler, Assembly assembly)
        {
            var permissions = new List<Permission>
            {
                new Permission {Description = "Service", Name = "Service", Seviye = 1},
                new Permission {Description = "UI", Name = "UI", Seviye = 1}
            };
         
            permissions.AddRange(menulerVeAltMenuler.Select(x => new { x.PermissionName, x.PermissionDescription }).Distinct().Select(x => new Permission()
            {
                Seviye = x.PermissionName.Split('.').Length,
                Description = x.PermissionDescription,
                Name = x.PermissionName
            }).ToList());

            permissions.AddRange(
            assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)) // Sadece ControllerBase türevlerini al
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => m.IsPublic && m.DeclaringType != null)
                          .Select(x => x.Name.Replace("Controller", "")).Distinct()
                          .Select(x => new Permission()
                          {
                              Description = x,
                              Name = "Service." + x,
                              Seviye = 2
                          }));
            return permissions;
        }
    }



}
