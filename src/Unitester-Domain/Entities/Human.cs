using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities;

public class Human:Auditable
{
    [MaxLength(50)]
    public string FirsName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string LastName { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string Region { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;
}
