using Bookify.Domain.Entities.Authorization;
using Bookify.Domain.Users;
using Bookify.Infrastructure;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class UserDataSeeds
  {
    public static void Seed(ApplicationDbContext ctx)
    {
      var roles = ctx.Set<Role>().ToList();
      var users = ctx.Set<User>().ToList();

      var adminRolYetkileri = new List<UserRole>();

      foreach (var user in users)
      {
        adminRolYetkileri.Add(new UserRole
        {
          UserId = user.Id,
          RoleId = roles.FirstOrDefault(x => x.Name == user.Roles.FirstOrDefault().Name).Id
        });
      };

      UpsertYetki(ctx, adminRolYetkileri);
    
    }
    private static void UpsertYetki(ApplicationDbContext ctx, List<UserRole> userRoles)
    {
      ctx.Upsert(userRoles, (x, y) => y.UserId == x.UserId);
      //ctx.SaveChanges();
    }
  }
}
