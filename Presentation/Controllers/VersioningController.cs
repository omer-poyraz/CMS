using Entities.DTOs.VersioningDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Versioning")]
    public class VersioningController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VersioningController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetOneVersioningByIdAsync()
        {
            try
            {
                var content = await _manager.VersioningService.GetVersioningByIdAsync();
                return Ok(ApiResponse<VersioningDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        // [AuthorizePermission("Versioning", "Update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneVersioningAsync()
        {
            try
            {
                var content = await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<VersioningDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}