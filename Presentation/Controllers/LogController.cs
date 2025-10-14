using System.Text.Json;
using Entities.DTOs.LogDto;
using Entities.RequestFeature.LogEntry;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Log işlemleri için controller
    /// </summary>
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

        /// <summary>
        /// Tüm log kayıtlarını getirir
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Log/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm log kayıtlarını listeler.
        ///     Yetkilendirme gerektirir.
        /// </remarks>
        /// <response code="200">Log kayıtları başarıyla getirildi</response>
        /// <response code="401">Yetkisiz erişim</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Log", "Read")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllLogsAsync([FromQuery] LogEntryParameters logParameters)
        {
            try
            {
                var logs = await _manager.LogService.GetAllLogsAsync(logParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(logs.metaData));
                return Ok(ApiResponse<IEnumerable<LogDto>>.CreateSuccess(_httpContextAccessor, logs.logDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip log kaydını getirir
        /// </summary>
        /// <param name="id">Log kaydının ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Log/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip log kaydını getirir.
        ///     Yetkilendirme gerektirir.
        /// </remarks>
        /// <response code="200">Log kaydı başarıyla getirildi</response>
        /// <response code="401">Yetkisiz erişim</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="404">Log kaydı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Log", "Read")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        /// <summary>
        /// Belirtilen ID'ye sahip log kaydını siler
        /// </summary>
        /// <param name="id">Silinecek log kaydının ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/Log/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip log kaydını sistemden siler.
        ///     Yetkilendirme gerektirir.
        ///     Silinen kayıt geri alınamaz.
        /// </remarks>
        /// <response code="200">Log kaydı başarıyla silindi</response>
        /// <response code="401">Yetkisiz erişim</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="404">Log kaydı bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Log", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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