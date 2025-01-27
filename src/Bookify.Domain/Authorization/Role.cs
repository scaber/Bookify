using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace Bookify.Domain.Entities.Authorization
{
    public class Role : Entity
    {
        //public static readonly Role Registered = new(Guid.NewGuid(), "Registered");

        //public Role(Guid id, string name,DateTime dateTime)
        //{
        //    Id = id;
        //    Name = name;
        //    ValidityDate=dateTime;
        //}
        [Required]
        [StringLength(50, ErrorMessage = "Rol adı 50 karakterden fazla olamaz.")]

         public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ValidityDate { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<RolePermission> RolePermission { get; set; } = new List<RolePermission>();
    }
}
