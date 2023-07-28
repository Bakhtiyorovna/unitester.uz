using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitester_DataAccess.Utils;
using Unitester_Service.Dtos.Contets;
using Unitester_Service.Interfaces.Contest;

namespace Unitester_api.Controllers
{
    [Route("api/contests")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _service;
        private readonly int maxPageSize = 30;
        public ContestController(IContestService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] ContestCreateDto dto)
       => Ok(await _service.CreateAsync(dto));
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => (Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize))));

        [HttpGet("{contestId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTeacherIdAsync(long contestId)
            => Ok(await _service.GetByIdAsync(contestId));

        [HttpPut("{ContestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long contestId, [FromForm] ContestUpdatedDto dto)
            => Ok(await _service.UpdateAsync( contestId, dto ));

        [HttpDelete("{contestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long contestId)
            => Ok(await _service.DeleteAsync(contestId));

        [HttpPost("Contest-register")]
        public async Task<IActionResult> RegisterContest(RegisterContestDto dto)
            => Ok(await _service.RegesterContestAsync(dto));
    }
}
