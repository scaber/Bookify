using Asp.Versioning;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Authorization.RolePermissionBooking;
using Bookify.Application.Authorization.RolePermissionTreeBooking;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Authorization;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/rolePermissionTree")]
public class RolePermissionTreeController : ControllerBase
{
    private readonly ISender _sender;

    public RolePermissionTreeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetRolePermissionTree(
     DateOnly startDate,
     DateOnly endDate,
     CancellationToken cancellationToken)
    {
        var query = new SearchRolePermissionTreeQuery(startDate, endDate);

        Result<IReadOnlyList<RolePermissionTreeResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
