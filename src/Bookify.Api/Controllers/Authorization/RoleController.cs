using Asp.Versioning;
using Bookify.Application.Authorization.RoleBooking;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Authorization;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/role")]
public class RoleController : ControllerBase
{

    private readonly ISender _sender;

    public RoleController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetRole(
     DateOnly startDate,
     DateOnly endDate,
     CancellationToken cancellationToken)
    {
        var query = new SearchRolesQuery();

        Result<IReadOnlyList<RoleResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
    [HttpPost]
    public async Task<IActionResult> AddRole(
         RoleRequest request,
         CancellationToken cancellationToken)
    {
        var command = new RoleComand(
            request.Name,
            request.Description,
            request.ValidityDate);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetRole), new { id = result.Value }, result.Value);
    }

}

