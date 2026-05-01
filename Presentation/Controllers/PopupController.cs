using Entities.DTOs.PopupDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Popup")]
    public class PopupController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PopupController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllPopupsAsync()
        {
            try
            {
                var contents = await _manager.PopupService.GetAllPopupsAsync(false);
                return Ok(ApiResponse<IEnumerable<PopupDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetOnePopupByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.PopupService.GetPopupByIdAsync(id, false);
                return Ok(ApiResponse<PopupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateOnePopupAsync([FromBody] PopupDtoForInsertion popupDtoForInsertion)
        {
            try
            {
                var content = await _manager.PopupService.CreatePopupAsync(popupDtoForInsertion);
                return Ok(ApiResponse<PopupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOnePopupAsync([FromBody] PopupDtoForUpdate popupDtoForUpdate)
        {
            try
            {
                var content = await _manager.PopupService.UpdatePopupAsync(popupDtoForUpdate);
                return Ok(ApiResponse<PopupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOnePopupAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.PopupService.DeletePopupAsync(id, false);
                return Ok(ApiResponse<PopupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}