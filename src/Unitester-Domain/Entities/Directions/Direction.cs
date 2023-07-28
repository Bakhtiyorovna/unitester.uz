using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities.Directions;

public class Direction : Auditable
{
    public string Name { get; set; }
    public DirectionType Type { get; set; }
    public string Description { get; set; } = String.Empty;

}
