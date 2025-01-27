using Bookify.Infrastructure;
using Bookify.Domain.Entities.Authorization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class PermissionDataSeeds
    {


        public static void Seed(ApplicationDbContext applicationDbContext, Assembly assembly)
        {
            var menus = MenuDataSeeds.MenuListesi;
            var menuTreeView = menus.Flatten(x => x.SubMenus).ToList();

            var permissions = PermissionList(menuTreeView, assembly);
            var permissionList = new List<Permission>(permissions);

            permissions.ForEach(yetki =>
            {
                if (yetki.Seviye != 1)
                {
                    var split = yetki.Name.Split('.');
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

            DeleteYetki(applicationDbContext, permissionList);
            UpsertYetki(applicationDbContext, permissionList);

        }
        /// <summary>
        /// Database'deki "Adı" listedekiler ile eşleşmeyenleri siler.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="yetkiList"></param>
        private static void DeleteYetki(ApplicationDbContext ctx, List<Permission> yetkiList)
        {
            ctx.Delete<Permission>(x => !yetkiList.Select(i => i.Name).Contains(x.Name));
            ctx.SaveChanges();
        }

        /// <summary>
        /// Listedeki "Adı" ve database'deki eşleşen verileri [Update] eder, listedeki "Adı" database'de yok ise [Insert] Eder.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="yetkiList"></param>
        private static void UpsertYetki(ApplicationDbContext ctx, List<Permission> yetkiList)
        {
            ctx.Upsert(yetkiList, (x, y) => y.Name == x.Name);
            ctx.SaveChanges();
        }

        private static List<Permission> PermissionList(List<MenuSeedModel> menulerVeAltMenuler, Assembly assembly)
        {
            var yetkiler = new List<Permission>
            {
                new Permission {Description = "Service", Name = "Service", Seviye = 1},
                new Permission {Description = "UI", Name = "UI", Seviye = 1}
            };
         
            yetkiler.AddRange(menulerVeAltMenuler.Select(x => new { x.PermissionName, x.PermissionDescription }).Distinct().Select(x => new Permission()
            {
                Seviye = x.PermissionName.Split('.').Length,
                Description = x.PermissionDescription,
                Name = x.PermissionName
            }).ToList());

            yetkiler.AddRange(
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
            return yetkiler;
        }
    }



}
