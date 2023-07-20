
namespace Unitester_Domain.Entities.Contests;

public class ContestStudent : Auditable
{
    public long ContestId { get; set; }
    public long StudentId { get; set; }
    public long BasicDirectionId { get; set; }
    public long SecondDirectionId { get; set; }
    public short TotalResult { get; set; }
    public string Description { get; set; } = String.Empty;
}
