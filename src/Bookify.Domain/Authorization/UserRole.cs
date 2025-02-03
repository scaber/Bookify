using System;
using System.Collections.Generic;
using System.Text;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Domain.Authorization
{
  public class UserRole : Entity
  {
    public Guid UserId { get; set; }
    public   User User { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
  }
}
