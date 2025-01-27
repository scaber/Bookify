﻿ 
using Asp.Versioning;
using Bookify.Application.Abstractions.Caching;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Menu;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Menu;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/menus")]
public class MenusController : ControllerBase
{
    private readonly ISender _sender;

    public MenusController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenus(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var query = new MenusQuery();

        Result<IReadOnlyList<  MenuResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
