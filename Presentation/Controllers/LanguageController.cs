using Entities.DTOs.LanguageDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Language")]
    public class LanguageController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllLanguagesAsync([FromQuery] string lang)
        {
            try
            {
                var contents = await _manager.LanguageService.GetAllLanguagesAsync(lang, false);
                return Ok(ApiResponse<IEnumerable<LanguageDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetOneLanguageByIdAsync([FromRoute] int id, [FromQuery] string lang)
        {
            try
            {
                var content = await _manager.LanguageService.GetLanguageByIdAsync(id, lang, false);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateOneLanguageAsync([FromBody] LanguageDtoForInsertion languageDtoForInsertion)
        {
            try
            {
                var content = await _manager.LanguageService.CreateLanguageAsync(languageDtoForInsertion);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOneLanguageAsync([FromBody] LanguageDtoForUpdate languageDtoForUpdate)
        {
            try
            {
                var content = await _manager.LanguageService.UpdateLanguageAsync(languageDtoForUpdate);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOneLanguageAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.LanguageService.DeleteLanguageAsync(id, false);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}