using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unitester_Domain.Enums;
using Unitester_Service.Dtos.Auth;
using Unitester_Service.Interfaces.Auth;
using Unitester_Service.Services.Auth;
using Unitester_Service.Validators;
using Unitester_Service.Validators.Dtos.Auth;

namespace Unitester_api.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new { result.Result, result.CachedMinutes });
        }

        //[HttpPost("register/send-code")]
        //public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        //{
        //    var result = PhoneNumberValidator.IsValid(phone);
        //    if (result == false) return BadRequest("Telefon raqam topilmadi!");

        //    var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
        //    return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        //}

        //[HttpPost("register/verify")]
        //public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        //{
        //    var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);
        //    return Ok(new { serviceResult.Result, serviceResult.Token });
        //}

        [HttpPost("register/send-code")]
        public async Task<IActionResult> SendCodeRegisterAsync(string email)
        {
            var result = await _authService.SendCodeForRegisterAsync(email);
            return Ok(new { result.Result, result.CachedVerificationMinutes });
        }

        [HttpPost("register/verify")]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.Email, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           UserRole identityRole = new UserRole();
            identityRole = UserRole.Pupil;
            return Ok(identityRole.ToString());
        }
    }
}
