using Bookify.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Authorization.RolePermissionTreeBooking;

public sealed class RolePermissionTreeResponse
{
    public Guid? Id { get; set; }
    public Guid? RoleId { get; set; }
    public string RoleName { get; set; }
    public Guid? PermissionId { get; set; }
    public string PermissionName { get; set; }
    public bool? Read { get; set; }
    public bool? Write { get; set; }
    public bool? Delete { get; set; }
    public int Level { get; set; }
    public string ParentName { get; set; }
    public List<RolePermissionTreeResponse> SubAuthorities { get; set; }
    public bool Checked { get; set; }
}
