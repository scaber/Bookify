using Bookify.Application.Abstractions.Caching;
using Microsoft.AspNetCore.Authorization;  
namespace Bookify.Infrastructure.Authorization;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement( )
    { 
    }
     
}
