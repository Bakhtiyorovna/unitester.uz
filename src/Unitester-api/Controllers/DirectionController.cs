using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unitester_DataAccess.Utils;
using Unitester_Service.Interfaces.Directions;

namespace Unitester_api.Controllers
{
    [Route("api/directions")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionService _service;
        private readonly int maxPageSize = 30;
        public DirectionController(IDirectionService service)
        {
            this._service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => (Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize))));
    }
}
