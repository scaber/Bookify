using Bookify.Application.Abstractions.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
using Bookify.Domain.Users;
using Bookify.Domain.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Infrastructure.Authorization;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement()
    {   
    } 
}
