namespace Bookify.Application.Authorization.RoleBooking;

public sealed class RoleResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }

    public string Currency { get; init; }
     
}
