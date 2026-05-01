using Entities.DTOs.SettingsDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Settings")]
    public class SettingsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SettingsController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetOneSettingsByIdAsync([FromQuery] string? lang)
        {
            try
            {
                var user = await _manager.SettingsService.GetSettingsAsync(lang, false);
                return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOneUserAsync([FromBody] SettingsDtoForUpdate settingsDtoForUpdate)
        {
            try
            {
                var user = await _manager.SettingsService.UpdateSettingsAsync(settingsDtoForUpdate);
                return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
