using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Authorization.RoleBooking;

public sealed record SearchRolesQuery() : IQuery<IReadOnlyList<RoleResponse>>;
