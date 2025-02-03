using Bookify.Domain.Authorization;

namespace Bookify.Domain.Users;

public sealed class RoleSilinecek
{
    public static readonly RoleSilinecek Registered = new(1, "Registered");

    public RoleSilinecek(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }

    public ICollection<User> Users { get; init; } = new List<User>();

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
}
