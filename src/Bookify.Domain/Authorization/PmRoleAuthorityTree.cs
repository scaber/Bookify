using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Bookify.Domain.Authorization
{
  public class PmRolePermissionTree : Entity
    { 
    public int RoleId { get; set; }
    public List<PmRolePermissionTreeList> RolePermissionList { get; set; } = new List<PmRolePermissionTreeList>();
  }

  public class PmRolePermissionTreeList
  {
    public int Id { get; set; }
    public string Text { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
    public bool Delete { get; set; }
    public int PermissionId { get; set; }
  }
}
