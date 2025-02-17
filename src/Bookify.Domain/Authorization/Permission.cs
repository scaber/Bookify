using Bookify.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Domain.Authorization
{
    public class Permission : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Seviye { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
