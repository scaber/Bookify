
using Asp.Versioning;
using Bookify.Application.Abstractions.Caching;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Menus;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Menu;

 
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
    public async Task<IActionResult> GetMenus(CancellationToken cancellationToken)
    {
        var query = new MenusQuery();

        Result<IReadOnlyList<MenuResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
