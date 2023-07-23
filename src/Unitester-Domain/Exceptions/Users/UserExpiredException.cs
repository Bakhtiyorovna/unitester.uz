

namespace Unitester_Domain.Exceptions.Users;

public class UserExpiredException:ExpiredException
{
    public UserExpiredException()
    {
        TitleMessage = "Tasdiqlash kodining muddati tugagan!";
    }
}
