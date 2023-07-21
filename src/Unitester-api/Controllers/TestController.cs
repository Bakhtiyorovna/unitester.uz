using Microsoft.AspNetCore.Mvc;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Enums;
using Unitester_Service.Dtos.Tests;
using Unitester_Service.Interfaces.Tests;
using Unitester_Service.Validators.Dtos.Tests;

namespace Unitester_api.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController:ControllerBase
    {
        private readonly ITestService _service;
        private readonly int maxPageSize = 30;
        public TestController(ITestService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] TestCreatedDto dto)
        {
            var createValidator = new TestCreatedValidator();
            var result = createValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypeAsync([FromQuery] int page = 1, TestType type=0)
        => (Ok(await _service.GetAllTypeAsync(new PaginationParams(page, maxPageSize),type))); 
        

    }
}
