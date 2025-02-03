using Asp.Versioning;
using Bookify.Api.Controllers.Bookings;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Authorization.RolePermissionBooking;
using Bookify.Application.Bookings.ReserveBooking;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Bookify.Api.Controllers.Authorization
{


    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/rolePermission")]
    public class RolePermissionController : ControllerBase
    {
        private readonly ISender _sender;

        public RolePermissionController(ISender sender)
        {
            _sender = sender;
        } 

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RolePermissionRequest request,CancellationToken cancellationToken)
        {
            var command = new UpdateRolePermissionCommand(
                request.RoleId,
                request.PermissionId,
                request.Read,
                request.Write,
                request.Delete
            );
            Result<bool> result = await _sender.Send(command, cancellationToken);


            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok("RolePermission updated successfully.");
        }

    }
}
