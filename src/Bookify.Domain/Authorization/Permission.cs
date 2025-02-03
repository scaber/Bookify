using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookify.Domain.Authorization
{
    public class Permission:Entity
    { 
        [Required]
        [StringLength(300, ErrorMessage = "Permission adı 300 karakterden fazla olamaz.")]
        public string Name { get; set; }
        [StringLength(550, ErrorMessage = "Permission açıklaması 550 karakterden fazla olamaz.")]
        public string Description { get; set; }
        public int Seviye { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
