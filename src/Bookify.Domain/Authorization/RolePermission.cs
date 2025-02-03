using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Authorization
{
    public sealed class RolePermission : Entity
    {
    

        public Role Role { get; set; }
        public Permission Permission { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public bool Read { get; set; } = false;
        public bool Write { get; set; }
        public bool Delete { get; set; } 
    }
}
