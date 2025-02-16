using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Authorization
{
    public class Role : Entity
    {
        public Role()
        {
            
        }
        public Role(Guid id, string name, string description, DateTime validityDate) : base(id)
        {
            Name = name;
            Description = description;
            ValidityDate = validityDate;
        }

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
        public List<UserRole> UserRoles { get; set; } = [];
        public List<RolePermission> RolePermission { get; set; } = [];


        public static Role CreateRole(string name, string description, DateTime validityDate)
        { 
            var role = new Role(
                Guid.NewGuid(),
               name,
               description,
               validityDate
               );
            return role;
        }
    }
}
