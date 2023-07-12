using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities.Users;

public class User:Human
{
    public UserRole Role { get; set; }
    public string Email { get; set; } = String.Empty;
    [MaxLength(13)]
    public string PhoneNumber { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
}
