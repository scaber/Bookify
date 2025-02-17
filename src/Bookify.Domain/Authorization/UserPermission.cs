using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Domain.Authorization;

public class UserPermission: Entity
{
     public User User { get; set; }
    public Permission Permission { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
    [Required]
    public bool Read { get; set; }
    [Required]
    public bool Write { get; set; }
    [Required]
    public bool Delete { get; set; }
}
