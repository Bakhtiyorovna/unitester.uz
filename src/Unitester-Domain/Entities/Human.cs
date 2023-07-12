using System.ComponentModel.DataAnnotations;
using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities;

public class Human:Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string LastName { get; set; }
    public string UserName { get; set; } = String.Empty;
    public Regions Region { get; set; } 
    public string ImagePath { get; set; } = String.Empty;
}
