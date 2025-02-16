using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Authorization.RoleBooking;



public sealed record RoleRequestCommand(
string Name,
string Description,
DateOnly ValidityDate );
