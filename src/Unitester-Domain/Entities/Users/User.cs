using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities.Users;

public class User : Human
{
    public UserRole Rol { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string Email { get; set; } = String.Empty;

    public string PasswordHash { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public DateTime LastActivity { get; set; }
}
