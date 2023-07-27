
using Unitester_Domain.Entities.Users;

namespace Unitester_Service.Interfaces.Auth;

public interface ITokenService
{
    public string GeneratedToken(User? user);
}
