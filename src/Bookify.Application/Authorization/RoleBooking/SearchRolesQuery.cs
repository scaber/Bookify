using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Authorization.RoleBooking;

public sealed record SearchRolesQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IReadOnlyList<RoleResponse>>;
