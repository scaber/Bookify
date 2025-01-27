using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Entities.Authorization
{ 
  public class RolePermissionResponse : Entity
  {
    public string RoleName { get; set; }
    public string PermissionName { get; set; }

  }

}
