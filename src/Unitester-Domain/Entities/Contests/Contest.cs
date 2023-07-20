using Unitester_Domain.Enums;
namespace Unitester_Domain.Entities.Contests;

public class Contest : Auditable
{
    public DateTime StartedAt { get; set; }
    public DateTime EndAt { get; set; }
    public ContestStatus Status { get; set; }
    public long StudentNumber { get; set; }
    public string Description { get; set; } = String.Empty;
}
