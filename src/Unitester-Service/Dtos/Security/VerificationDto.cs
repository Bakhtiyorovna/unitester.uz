using Unitester_Service.Dtos.Auth;
namespace Unitester_Service.Dtos.Security;

public class VerificationDto
{
    public int Code { get; set; }

    public int Attempt { get; set; }

    public DateTime CreatedAt { get; set; }
}
