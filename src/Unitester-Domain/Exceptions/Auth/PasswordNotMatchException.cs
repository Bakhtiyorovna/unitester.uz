

namespace Unitester_Domain.Exceptions.Auth;

public class PasswordNotMatchException :BadRequestException
{
    public PasswordNotMatchException()
    {
        TitleMessage = "Parol yaroqsiz!";
    }
}
