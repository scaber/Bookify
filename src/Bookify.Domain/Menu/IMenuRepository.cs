using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Menu;

public interface IMenuRepository
{
    Task<Menu?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

