namespace Unitester_Service.Dtos.Contets;

public class ContetsCreatedDto
{
    public DateTime StartedAt { get; set; }
    public DateTime EndAt { get; set; }
    public string Description { get; set; } = String.Empty;
}
