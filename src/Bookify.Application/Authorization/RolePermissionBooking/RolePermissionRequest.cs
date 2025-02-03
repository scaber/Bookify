using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Authorization.RolePermissionBooking;

public sealed record RolePermissionRequest(
       Guid RoleId,
       Guid PermissionId,
       bool Read,
       bool Write,
       bool Delete
   ); 
