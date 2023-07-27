using Microsoft.Extensions.Caching.Memory;
using Unitester_DataAccess.Interfaces.Users;
using Unitester_Domain.Entities.Users;
using Unitester_Domain.Exceptions.Auth;
using Unitester_Domain.Exceptions.Users;
using Unitester_Service.Comman.Helpers;
using Unitester_Service.Comman.Security;
using Unitester_Service.Dtos.Auth;
using Unitester_Service.Dtos.Notification;
using Unitester_Service.Dtos.Security;
using Unitester_Service.Interfaces.Auth;
using Unitester_Service.Interfaces.Notification;

namespace Unitester_Service.Services.Auth;
public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private readonly ISmsSender _smsSender;
    private readonly ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    public string NoneAvatar = "media\\images\\Avatar.jpg";
    public AuthService(IMemoryCache memoryCache,
      IUserRepository userRepository,
      ISmsSender smsSender,
      ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
        this._smsSender = smsSender;
        _tokenService = tokenService;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByconstructionAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

        // delete if exists user by this phone number
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }


    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            // tasdiqlash kodini deneratsiya qilish
            verificationDto.Code = 1111;// CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage smsMessage = new SmsMessage();
            smsMessage.Title = "Unitester" + "\n";
            smsMessage.Content = "Sizning tasdiqlash kodingiz: " + verificationDto.Code;
            smsMessage.Recipent = phone.Substring(1);

            var smsResult = await _smsSender.SendAsync(smsMessage);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterPupilAsync(registerDto);
                    // if (dbResult is true)
                    if (dbResult is true)
                    {
                        var user = await _userRepository.GetByconstructionAsync(phone);
                        string token = _tokenService.GeneratedToken(user);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }


    private async Task<bool> RegisterPupilAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.UserName = registerDto.UserName;
        user.Email = registerDto.Email;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.Rol = Unitester_Domain.Enums.UserRole.Teacher;
        user.PhoneNumberConfirmed = true;

        var hasherResult = PasswordHasher.Hash(registerDto.Password);
        user.Password = hasherResult.Hash;
        user.Salt = hasherResult.Salt;
        user.ImagePath = "media\\images\\Avatar.jpg";

        user.CreatedAt = user.UpdatedAt = user.LastActivity = TimeHelper.GetDateTime();
        user.Rol = Unitester_Domain.Enums.UserRole.Pupil;

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }

    public async Task<bool> RegisterTeacherAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.UserName = registerDto.UserName;
        user.Email = registerDto.Email;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.Rol = Unitester_Domain.Enums.UserRole.Teacher;
        user.PhoneNumberConfirmed = true;
        user.ImagePath = NoneAvatar;
        var hasherResult = PasswordHasher.Hash(registerDto.Password);
        user.Password = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        user.CreatedAt = user.UpdatedAt = user.LastActivity = TimeHelper.GetDateTime();
        user.Rol = Unitester_Domain.Enums.UserRole.Teacher;

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByconstructionAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.Password, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GeneratedToken(user);
        return (Result: true, Token: token);
    }
}