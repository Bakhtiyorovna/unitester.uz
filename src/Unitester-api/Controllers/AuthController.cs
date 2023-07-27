using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new { result.Result, result.CachedMinutes });
        }

       
        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Telefon raqam topilmadi!");

            var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }


        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.Construction, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpPost("register/teacher")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterTeacherAsync([FromForm] RegisterDto dto)
         => Ok(await _authService.RegisterTeacherAsync(dto));

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto logindto)
        {
            //var validator = new LoginValidator();
            //var valResult = validator.Validate(logindto);

            //if (valResult.IsValid == false) return BadRequest(valResult.Errors);

            var serviceResult = await _authService.LoginAsync(logindto);
            return Ok(new {serviceResult.Result, serviceResult.Token});
        }
    }
}
