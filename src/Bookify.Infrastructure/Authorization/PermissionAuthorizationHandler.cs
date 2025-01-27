using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Bookify.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionAuthorizationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.Resource != null)
        {
            Debug.WriteLine(context.Resource.GetType());
        }

        var mvcContext = context.Resource as AuthorizationFilterContext;

        if (mvcContext?.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            if (descriptor.ControllerTypeInfo.BaseType == typeof(ControllerBase))
            {
                var method = mvcContext.HttpContext.Request.Method.ToLower();
                var ctrlName = descriptor.ControllerName;
                if (ctrlName == "User")
                {
                    var path = mvcContext.HttpContext.Request.Path.HasValue ? mvcContext.HttpContext.Request.Path.Value : null;
                    if (path != null)
                    {
                        var splittedPath = path.Split('/');
                        var id = splittedPath[^1];
                        try
                        {
                            System.Security.Claims.ClaimsIdentity claimsIdentity = mvcContext.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
                            var userid = claimsIdentity.FindFirst(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                            if (userid == id && method == "put")
                            {
                                context.Succeed(requirement);
                                return;
                            }
                        }
                        catch { }
                    }
                }
    
                var user_id = context.User.FindAll("user_id").FirstOrDefault(); 

                if (Guid.TryParse(user_id.Value, out Guid userIdGuid))
                {
                    // Successfully parsed userIdGuid
                }
                else
                {
                    // Handle the case where parsing fails
                    context.Fail();
                    return;
                }

                using IServiceScope scope = _serviceProvider.CreateScope();

                AuthorizationService authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

                UserRolePermissionResponse permission = await authorizationService.GetUserRolePermissionAsync(ctrlName, userIdGuid);

                if (permission == null)
                    context.Fail();

                if (permission != null)
                {
                    switch (method)
                    {
                        case "get":
                            if (permission.CanRead)
                                context.Succeed(requirement);
                            else
                                context.Fail();
                            break;

                        case { } n when (n == "put" || n == "post" || n == "patch"):
                            if (permission.CanWrite)
                                context.Succeed(requirement);
                            else
                                context.Fail();
                            break;

                        case "delete":
                            if (permission.CanDelete)
                                context.Succeed(requirement);
                            else
                                context.Fail();
                            break;
                    }
                }
            }
            else
            {
                context.Succeed(requirement);
            }
        }
        else
        {
            context.Succeed(requirement);
        }

        if (context.Resource is RouteEndpoint pageContext)
        {
            context.Succeed(requirement);
        }
    }

}
