using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitester_DataAccess.Utils;
using Unitester_Service.Dtos.Users;
using Unitester_Service.Interfaces.Users;
namespace Unitester_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly int maxPageSize = 30;
        public UsersController(IUserService service)
        {
            this._service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
           => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTeacherIdAsync(long userId)
            => Ok(await _service.GetByIdAsync(userId));

        [HttpGet("count/techer")]
        [AllowAnonymous]
        public async Task<IActionResult> CountTeacherAsync()
            => Ok(await _service.CountRoleAsync("1"));

        [HttpGet("count/pupil")]
        [AllowAnonymous]
        public async Task<IActionResult> CountPupilAsync()
           => Ok(await _service.CountRoleAsync("2"));


        [HttpPut("{teacherId}")]
        [Authorize(Roles = "Admin , Teacher")]
        public async Task<IActionResult> UpdateAsync(long teacherId, [FromForm] UserUpdateDto dto)
            => Ok(await _service.UpdateAsync(teacherId, dto));

        [HttpDelete("{userd}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteAsync(long userId)
            => Ok(await _service.DeleteAsync(userId));
    }
}
