
namespace Unitester_Domain.Exceptions.Auth;

public class VerificationCodeExpiredException:ExpiredException
{
    public VerificationCodeExpiredException()
    {
        TitleMessage = "Tasdiqlash kodining muddati tugagan!";
    }
}
