using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.Authorization.RolePermissionTreeBooking;

namespace Bookify.Api.Controllers.Authorization;

public sealed record SearchRolePermissionTreeQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IReadOnlyList<RolePermissionTreeResponse>>;
 

 