using Bookify.Domain.Apartments;
using Bookify.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Repositories ;

    internal sealed class MenuRepository : Repository<Menu>, IMenuRepository
{
        public MenuRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
 
