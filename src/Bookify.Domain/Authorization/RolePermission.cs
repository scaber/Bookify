using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Bookify.Domain.Entities.Authorization
{
  public class RolePermission:Entity
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
