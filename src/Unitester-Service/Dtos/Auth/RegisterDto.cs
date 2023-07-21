using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;
using Microsoft.AspNetCore.Http;
namespace Unitester_Service.Dtos.Auth;

public class RegisterDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; }
    public string UserName { get; set; } = String.Empty;
    public Regions Region { get; set; }
    public IFormFile Image { get; set; } = default!;
    public UserRole Rol { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
}
