
using BCrypt;
namespace Unitester_Service.Comman.Security;

public class PasswordHasher
{
    public static (string Hash, string Salt) Hash(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
        return (Hash: hash, Salt: salt);
    }
}
