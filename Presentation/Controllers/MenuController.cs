using Entities.DTOs.MenuDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Menu")]
    public class MenuController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllMenusAsync([FromQuery] string lang)
        {
            try
            {
                var contents = await _manager.MenuService.GetAllMenusAsync(lang, false);
                return Ok(ApiResponse<IEnumerable<MenuDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<IEnumerable<MenuDto>>.CreateError(_httpContextAccessor, "Error.ServerError"));
            }
        }

        [HttpGet("GetAllByGroup/{id:int}")]
        public async Task<IActionResult> GetAllMenusAsync([FromRoute] int id, [FromQuery] string lang)
        {
            try
            {
                var contents = await _manager.MenuService.GetAllMenusByGroupAsync(id, lang, false);
                return Ok(ApiResponse<IEnumerable<MenuDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<IEnumerable<MenuDto>>.CreateError(_httpContextAccessor, "Error.ServerError"));
            }
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetOneMenuByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.MenuService.GetMenuByIdAsync(id, false);
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.NotFound", 404));
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateOneMenuAsync([FromBody] MenuDtoForInsertion menuDtoForInsertion)
        {
            try
            {
                var content = await _manager.MenuService.CreateMenuAsync(menuDtoForInsertion);
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception)
            {
                return BadRequest(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.ServerError", 400));
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOneMenuAsync([FromBody] MenuDtoForUpdate menuDtoForUpdate)
        {
            try
            {
                var content = await _manager.MenuService.UpdateMenuAsync(menuDtoForUpdate);
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.NotFound", 404));
            }
        }

        [HttpPut("Sort/{id:int}/{sort:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> SortMenuAsync([FromRoute] int id, int sort)
        {
            try
            {
                var content = await _manager.MenuService.SortMenuAsync(id, sort, false);
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOneMenuAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.MenuService.DeleteMenuAsync(id, false);
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.NotFound", 404));
            }
        }
    }
}