using System.Text.Json;
using Entities.DTOs.SettingsDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Çoklu site/şirket ayarlarının yönetimi için controller
    /// </summary>
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

        /// <summary>
        /// Mevcut site/şirket ayarlarını getirir
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Settings/Get
        ///     
        ///     Bu endpoint mevcut site/şirket ayarlarını getirir.
        ///     Detaylı ayar bilgileri:
        ///     - Şirket adı (çoklu dil)
        ///     - İletişim bilgileri (çoklu dil)
        ///     - Adres bilgileri (çoklu dil)
        ///     - Logo ve diğer görseller
        /// </remarks>
        /// <response code="200">Ayar kaydı başarıyla getirildi</response>
        /// <response code="400">Ayar kaydı bulunamadı</response>
        [HttpGet("Get")]
        public async Task<IActionResult> GetOneSettingsByIdAsync()
        {
            try
            {
                var user = await _manager.SettingsService.GetSettingsAsync(false);
                return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut site/şirket ayarlarını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Settings/Update
        ///     Content-Type: multipart/form-data
        ///     
        ///     {
        ///         "ID": 1,
        ///         "Name": {
        ///             "TR": "Güncellenmiş Şirket A",
        ///             "EN": "Updated Company A"
        ///         },
        ///         "Phone": {
        ///             "TR": "+90 212 987 6543",
        ///             "EN": "+1 555 987 6543"
        ///         },
        ///         "file": [binary_file_data] // Güncellenecek görseller
        ///     }
        /// </remarks>
        /// <response code="200">Ayarlar başarıyla güncellendi</response>
        /// <response code="403">Yetkisiz yetki</response>
        /// <response code="400">Geçersiz veri veya kayıt bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Settings", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
