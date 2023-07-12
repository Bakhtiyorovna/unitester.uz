using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;

namespace Unitester_Service.Dtos.Users;

public class UserUpdateDto
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string LastName { get; set; }
    public string UserName { get; set; } = String.Empty;
    //public Regions Region { get; set; }
    public IFormFile? Image { get; set; }
    //public UserRole Role { get; set; }
    public string Email { get; set; } = String.Empty;

    [MaxLength(13)]
    public string PhoneNumber { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
}
