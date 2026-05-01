using System.Text.Json;
using Entities.DTOs.PageDto;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.RequestFeature.Page;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Page")]
    public class PageController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PageController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllPagesAsync([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var pages = await _manager.PageService.GetAllPagesAsync(pageParameters, false);
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pages.metaData));
                return Ok(ApiResponse<IEnumerable<PageDto>>.CreateSuccess(_httpContextAccessor, pages.pageDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetOnePageByIdAsync([FromRoute] int id)
        {
            try
            {
                var page = await _manager.PageService.GetPageByIdAsync(id, false);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("GetSlug/{*slug}")]
        public async Task<IActionResult> GetOnePageBySlugAsync(string slug, string lang)
        {
            try
            {
                var page = await _manager.PageService.GetPageBySlugAsync(slug, lang, false);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateOnePageAsync([FromBody] PageDtoForInsertion pageDtoForInsertion)
        {
            try
            {
                var page = await _manager.PageService.CreatePageAsync(pageDtoForInsertion);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UpdateOnePageAsync([FromBody] PageDtoForUpdate pageDtoForUpdate)
        {
            try
            {
                var page = await _manager.PageService.UpdatePageAsync(pageDtoForUpdate);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOnePageAsync([FromRoute] int id)
        {
            try
            {
                var page = await _manager.PageService.DeletePageAsync(id, false);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
