using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Bookify.Domain.Entities.Authorization
{
  public class RolePermissionTree : Entity
  {
    public int? RoleId { get; set; }
    public string RoleName { get; set; }
    public int? PermissionId { get; set; }
    public string PermissionName { get; set; }
    public bool? Read { get; set; }
    public bool? Write { get; set; }
    public bool? Delete { get; set; }
    public int Level { get; set; }
    public string ParentName { get; set; }
    public List<RolePermissionTree> SubAuthorities { get; set; }
    public bool Checked { get; set; }
  }

 
}
