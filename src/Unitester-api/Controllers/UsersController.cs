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
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
           => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(long userId)
            => Ok(await _service.GetByIdAsync(userId));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] UserCreatedDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
            => Ok(await _service.UpdateAsync(userId, dto));

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(long userId)
            => Ok(await _service.DeleteAsync(userId));
    }
}
