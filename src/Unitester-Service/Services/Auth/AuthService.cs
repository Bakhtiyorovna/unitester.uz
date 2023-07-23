
using Microsoft.AspNetCore.Identity;
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
    private readonly IEmailSender _emailSender;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    public AuthService(IMemoryCache memoryCache,
      IUserRepository userRepository,
      ISmsSender smsSender,
      IEmailSender emailSender)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
        this._smsSender = smsSender;
        this._emailSender = emailSender;
    }

    //public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    //{
    //    var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
    //    if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

    //    //Agar ushbu telefon raqami bo'yicha foydalanuvchi mavjud bo'lsa, o'chiring
    //    if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
    //    {
    //        cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
    //        _memoryCache.Remove(dto.PhoneNumber);
    //    }
    //    else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
    //        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

    //    return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    //}

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var Costumer = await _userRepository.GetByEmailAsync(dto.Email);
        if (Costumer is not null) throw new UserAlreadyExistsException(dto.Email);

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.Email, out RegisterDto registrDto))
        {
            registrDto.Email = registrDto.Email;
            _memoryCache.Remove(dto.Email);
        }
        else
        {
            _memoryCache.Set(REGISTER_CACHE_KEY + dto.Email, dto, TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));
        }
        return (Result: true, CachedMinutes:CACHED_MINUTES_FOR_REGISTER);
    }


    //public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    //{
    //    if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
    //    {
    //        VerificationDto verificationDto = new VerificationDto();
    //        verificationDto.Attempt = 0;
    //        verificationDto.CreatedAt = TimeHelper.GetDateTime();

    //        // tasdiqlash kodini generatsiya qilish
    //        verificationDto.Code = CodeGenerator.GenerateRandomNumber();

    //        if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
    //        {
    //            _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
    //        }

    //        _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
    //            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

    //        SmsMessage smsMessage = new SmsMessage();
    //        smsMessage.Title = "UniTester";
    //        smsMessage.Content = "Your verification code : " + verificationDto.Code;
    //        smsMessage.Recipent = phone.Substring(1);

    //        var smsResult = await _smsSender.SendAsync(smsMessage);
    //        if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
    //        else return (Result: false, CachedVerificationMinutes: 0);
    //    }
    //    else throw new UserCacheDataExpiredException();
    //}

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registrDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = 1234;
            _memoryCache.Set(email, verificationDto, TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            // emal sende begin
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
            }
            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
            // emsil sender end 
            EmailMessage smsMessage = new EmailMessage();
            smsMessage.Title = "Course Zone";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = email;
            var result = await _emailSender.SenderAsync(smsMessage);
            if (result is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedMinutes: 0);
        }
        else
        {
            throw new UserExpiredException();
        }
    }

    //public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    //{

    //    if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
    //    {
    //        if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
    //        {
    //            if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
    //                throw new VerificationTooManyRequestsException();
    //            else if (verificationDto.Code == code)
    //            {
    //                var dbResult = await RegisterToDatabaseAsync(registerDto);
    //                return (Result: dbResult, Token: "");
    //            }
    //            else
    //            {
    //                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
    //                verificationDto.Attempt++;
    //                _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
    //                    TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
    //                return (Result: false, Token: "");
    //            }
    //        }
    //        else throw new VerificationCodeExpiredException();
    //    }
    //    else throw new UserCacheDataExpiredException();

    //}

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    return (Result: dbResult, Token: "");

                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
                    verificationDto.Attempt++;

                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;

        var hasherResult = PasswordHasher.Hash(registerDto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        user.CreatedAt = user.UpdatedAt = user.LastActivity = TimeHelper.GetDateTime();
        user.Rol = Unitester_Domain.Enums.UserRole.Pupil;

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }

    //private async Task<bool> RegisterToDatabaseAsync(RegistrDto registerDto)
    //{
    //    Customer customer = new Customer();
    //    customer.FullName = registerDto.FullName;
    //    customer.ImagePathCustomer = registerDto.ImagePathCustomer;
    //    customer.Email = registerDto.Email;
    //    customer.CreatedAt = TimeHelper.GetDateTime();
    //    customer.UpdatedAt = TimeHelper.GetDateTime();


    //    var dbResult = await _customerRepository.CreateAsync(customer);
    //    return dbResult > 0;
    //}
}
