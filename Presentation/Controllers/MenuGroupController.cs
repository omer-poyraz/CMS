using Entities.DTOs.MenuGroupDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/MenuGroup")]
    public class MenuGroupController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuGroupController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllMenuGroupsAsync([FromQuery] string lang)
        {
            try
            {
                var contents = await _manager.MenuGroupService.GetAllMenuGroupsAsync(lang, false);
                return Ok(ApiResponse<IEnumerable<MenuGroupDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetOneMenuGroupByIdAsync([FromRoute] int id, [FromQuery] string lang)
        {
            try
            {
                var content = await _manager.MenuGroupService.GetMenuGroupByIdAsync(id, lang, false);
                return Ok(ApiResponse<MenuGroupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateOneMenuGroupAsync([FromBody] MenuGroupDtoForInsertion menuGroupDtoForInsertion)
        {
            try
            {
                var content = await _manager.MenuGroupService.CreateMenuGroupAsync(menuGroupDtoForInsertion);
                return Ok(ApiResponse<MenuGroupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOneMenuGroupAsync([FromBody] MenuGroupDtoForUpdate menuGroupDtoForUpdate)
        {
            try
            {
                var content = await _manager.MenuGroupService.UpdateMenuGroupAsync(menuGroupDtoForUpdate);
                return Ok(ApiResponse<MenuGroupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOneMenuGroupAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.MenuGroupService.DeleteMenuGroupAsync(id, false);
                return Ok(ApiResponse<MenuGroupDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}