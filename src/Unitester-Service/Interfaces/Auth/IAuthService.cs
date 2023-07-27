
using Unitester_Domain.Entities.Users;
using Unitester_Service.Dtos.Auth;

namespace Unitester_Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<bool> RegisterTeacherAsync(RegisterDto dto);

    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);

    public Task<(bool Result, string Token)> LoginAsync(LoginDto dto);
}
