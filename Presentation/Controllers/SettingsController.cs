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
        /// Tüm site/şirket ayarlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Settings/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm site/şirket ayarlarını listeler.
        ///     Her site/şirket için:
        ///     - İletişim bilgileri
        ///     - Adres bilgileri
        ///     - Logo ve görsel ayarları
        ///     - Sosyal medya bilgileri
        /// </remarks>
        /// <response code="200">Ayarlar başarıyla listelendi</response>
        /// <response code="403">Yetkisiz yetki</response>
        /// <response code="400">İşlem başarısız</response>
        // [HttpGet("GetAll")]
        // [AuthorizePermission("Settings", "Read")]
        // public async Task<IActionResult> GetAllSettingssAsync()
        // {
        //     try
        //     {
        //         var users = await _manager.SettingsService.GetAllSettingsAsync(false);
        //         return Ok(ApiResponse<IEnumerable<SettingsDto>>.CreateSuccess(_httpContextAccessor, users, "Success.Listed"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }

        /// <summary>
        /// Belirtilen ID'ye sahip site/şirket ayarlarını getirir
        /// </summary>
        /// <param name="id">Ayar kaydı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Settings/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip site/şirket ayarlarını getirir.
        ///     Detaylı ayar bilgileri:
        ///     - Şirket adı (çoklu dil)
        ///     - İletişim bilgileri (çoklu dil)
        ///     - Adres bilgileri (çoklu dil)
        ///     - Logo ve diğer görseller
        /// </remarks>
        /// <response code="200">Ayar kaydı başarıyla getirildi</response>
        /// <response code="400">Ayar kaydı bulunamadı</response>
        // [HttpGet("GetSingle")]
        // // [AuthorizePermission("Settings", "Read")]
        // public async Task<IActionResult> GetOneSettingsAsync()
        // {
        //     try
        //     {
        //         var user = await _manager.SettingsService.GetSettingsAsync(false);
        //         return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }

        /// <summary>
        /// Belirtilen ID'ye sahip site/şirket ayarlarını getirir
        /// </summary>
        /// <param name="id">Ayar kaydı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Settings/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip site/şirket ayarlarını getirir.
        ///     Detaylı ayar bilgileri:
        ///     - Şirket adı (çoklu dil)
        ///     - İletişim bilgileri (çoklu dil)
        ///     - Adres bilgileri (çoklu dil)
        ///     - Logo ve diğer görseller
        /// </remarks>
        /// <response code="200">Ayar kaydı başarıyla getirildi</response>
        /// <response code="400">Ayar kaydı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Settings", "Read")]
        public async Task<IActionResult> GetOneSettingsByIdAsync([FromRoute] int id)
        {
            try
            {
                var user = await _manager.SettingsService.GetSettingsByIdAsync(id, false);
                return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni site/şirket ayarları oluşturur
        /// </summary>
        /// <param name="form">Form verisi (çoklu dil desteği için)</param>
        /// <param name="settingsDtoForInsertion">Ayar bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Settings/Create
        ///     Content-Type: multipart/form-data
        ///     
        ///     {
        ///         "Name": {
        ///             "TR": "Şirket A",
        ///             "EN": "Company A"
        ///         },
        ///         "Phone": {
        ///             "TR": "+90 212 123 4567",
        ///             "EN": "+1 555 123 4567"
        ///         },
        ///         "Address": {
        ///             "TR": "İstanbul, Türkiye",
        ///             "EN": "Istanbul, Turkey"
        ///         },
        ///         "file": [binary_file_data] // Logo ve diğer görseller
        ///     }
        /// </remarks>
        /// <response code="200">Ayarlar başarıyla oluşturuldu</response>
        /// <response code="403">Yetkisiz yetki</response>
        /// <response code="400">Geçersiz veri</response>
        // [HttpPost("Create")]
        // // [AuthorizePermission("Settings", "Write")]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        // public async Task<IActionResult> CreateOneSettingsAsync([FromBody] SettingsDtoForInsertion settingsDtoForInsertion)
        // {
        //     try
        //     {
        //         var user = await _manager.SettingsService.CreateSettingsAsync(settingsDtoForInsertion);
        //         await _manager.VersioningService.UpdateVersioningAsync();
        //         return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Created"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }

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
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Site/şirket ayarlarını siler
        /// </summary>
        /// <param name="id">Silinecek ayar kaydı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/Settings/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip site/şirket ayarlarını siler.
        ///     Dikkat:
        ///     - Silinen ayarlar geri alınamaz
        ///     - İlişkili tüm dosyalar (logo vb.) silinir
        ///     - Site/şirket ayarları olmadan ilgili site çalışmayabilir
        /// </remarks>
        /// <response code="200">Ayarlar başarıyla silindi</response>
        /// <response code="403">Yetkisiz yetki</response>
        /// <response code="400">Ayar kaydı bulunamadı</response>
        // [HttpDelete("Delete/{id:int}")]
        // // [AuthorizePermission("Settings", "Delete")]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        // public async Task<IActionResult> DeleteOneSettingsAsync([FromRoute] int id)
        // {
        //     try
        //     {
        //         var user = await _manager.SettingsService.DeleteSettingsAsync(id, false);
        //         await _manager.VersioningService.UpdateVersioningAsync();
        //         return Ok(ApiResponse<SettingsDto>.CreateSuccess(_httpContextAccessor, user, "Success.Deleted"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }
    }
}
