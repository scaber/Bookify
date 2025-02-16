using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Authorization.RoleBooking;


public sealed record RoleComand(
string Name,
string Description,
DateTime ValidityDate 
 ) : ICommand<Guid>;
