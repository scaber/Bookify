namespace Bookify.Application.Authorization.RoleBooking;

public sealed record RoleRequestCommand(
string Name,
string Description,
DateOnly ValidityDate);
