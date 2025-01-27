using Bookify.Infrastructure;
using System.Reflection;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public interface IDFiloSeeder
    {
        void SeedData(System.Reflection.Assembly asm);
        void Upsert(Assembly asm);

    }

    public class DFiloSeeder: IDFiloSeeder
    {
        private readonly ApplicationDbContext _ctx;

        public DFiloSeeder(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void SeedData(System.Reflection.Assembly asm)
        {
            RoleDataSeeds.Seed(_ctx);
            PermissionDataSeeds.Seed(_ctx, asm);
            MenuDataSeeds.Seed(_ctx);

            RolePermissionSeedsData.Seed(_ctx);
            //UserDataSeeds.Seed(_ctx);
        }

         
        public void Upsert(Assembly asm)
        {
            RoleDataSeeds.Seed(_ctx);
        }

    }
}
