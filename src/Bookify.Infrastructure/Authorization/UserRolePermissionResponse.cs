using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authorization
{
    internal sealed class UserRolePermissionResponse
    {
        public string Name { get; set; }
        public bool CanRead { get; set; }
        public bool CanDelete { get; set; }
        public bool CanWrite { get; set; }
    }
}
