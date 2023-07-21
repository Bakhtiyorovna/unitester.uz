
namespace Unitester_Domain.Exceptions.Auth;

public class VerificationTooManyRequestsException:TooManyRequestException
{
    public VerificationTooManyRequestsException()
    {
        TitleMessage = "Siz chegaradan ko'p marotaba urindingiz!";
    }
}
