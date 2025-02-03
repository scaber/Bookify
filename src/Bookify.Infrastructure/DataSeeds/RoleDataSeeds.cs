using Bookify.Domain.Authorization;
using Bookify.Infrastructure;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static class RoleDataSeeds
    {
        public static void Seed(ApplicationDbContext ctx)
        {
            var roleData = new List<Role>()
                                   {
                                new  Role
                                {
                                  Name = "domain_admin",
                                  Description = "Admin",
                                  ValidityDate = DateTime.UtcNow.AddYears(5)

                                },
                                new Role
                                {
                                  Name = "idari_isler",
                                  Description = "İdari İşler",
                                  ValidityDate = DateTime.UtcNow.AddDays(10)
                                }
                                  };

            UpsertRol(ctx, roleData);
        }
        private static void UpsertRol(ApplicationDbContext ctx, List<Role> rolData)
        {
            ctx.Upsert(rolData, (x, y) => y.Name == x.Name);
            ctx.SaveChanges();
        }


    }
}
