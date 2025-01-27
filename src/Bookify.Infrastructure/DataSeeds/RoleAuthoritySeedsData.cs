using Bookify.Domain.Entities.Authorization;
using Bookify.Infrastructure;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class RolePermissionSeedsData
  {

    public static void Seed(ApplicationDbContext ctx )
    {
      var admin = ctx.Set<Role>().FirstOrDefault(x => x.Name == "domain_admin");
      var yetkiler = ctx.Set<Permission>().ToList();
      var adminRolYetkileri = new List<RolePermission>();

      foreach (var yetki in yetkiler)
      {
        adminRolYetkileri.Add(new RolePermission
        { 
          Read = true,
          Write = true,
          Delete = true,
          RoleId = admin.Id,
          PermissionId = yetki.Id,
        });
      };

      DeleteRolYetkiByYetki(ctx, adminRolYetkileri);
      UpsertAdminRolYetki(ctx, adminRolYetkileri, admin.Id);
    }
    private static void DeleteRolYetkiByYetki(ApplicationDbContext ctx, List<RolePermission> rolYetkiler)
    {
      ctx.Delete<RolePermission>(x => !rolYetkiler.Select(i => i.PermissionId).Contains(x.PermissionId));
      ctx.SaveChanges();
    }

    /// <summary>
    /// Listedeki "YetkiId" ve database'deki eşleşen , database'deki Admin rolündeki RolYetki verileri [Update] eder, listedeki "YetkiId"-"Admin" ikilisi database'de yok ise [Insert] Eder.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="adminRolYetkileri"></param>
    /// <param name="adminRolId"></param>
    private static void UpsertAdminRolYetki(ApplicationDbContext ctx, List<RolePermission> adminRolYetkileri, Guid adminRolId)
    {
      ctx.Upsert(adminRolYetkileri, (x, y) => y.RoleId == adminRolId && y.PermissionId == x.PermissionId);
      ctx.SaveChanges();
    }
  }
}
