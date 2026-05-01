using System.Text.Json;
using Entities.DTOs.LogDto;
using Entities.RequestFeature.LogEntry;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Log")]
    public class LogController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllLogsAsync([FromQuery] LogEntryParameters logParameters)
        {
            try
            {
                var logs = await _manager.LogService.GetAllLogsAsync(logParameters, false);
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(logs.metaData));
                return Ok(ApiResponse<IEnumerable<LogDto>>.CreateSuccess(_httpContextAccessor, logs.logDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetOneLogByIdAsync([FromRoute] int id)
        {
            try
            {
                var log = await _manager.LogService.GetLogByIdAsync(id, false);
                return Ok(ApiResponse<LogDto>.CreateSuccess(_httpContextAccessor, log, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteOneLogAsync([FromRoute] int id)
        {
            try
            {
                var log = await _manager.LogService.DeleteLogAsync(id, false);
                return Ok(ApiResponse<LogDto>.CreateSuccess(_httpContextAccessor, log, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}