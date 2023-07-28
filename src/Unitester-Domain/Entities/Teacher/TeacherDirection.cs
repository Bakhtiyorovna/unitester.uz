namespace Unitester_Domain.Entities.Teacher;

public class TeacherDirection : Auditable
{
    public long TeacherId { get; set; }

    public long DirectionId { get; set; }

    public int TestNumber { get; set; }

    public string Description { get; set; } = String.Empty;
}
