using System.Text.Json;
using Entities.DTOs.LanguageDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS language içeriklerinin yönetimi için controller (language yanıtları, anket sonuçları vb.)
    /// </summary>
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

        /// <summary>
        /// Tüm language içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Language/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm language içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Language yanıtları
        ///     - Anket sonuçları
        ///     - İletişim languageu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Language", "Read")]
        public async Task<IActionResult> GetAllLanguagesAsync()
        {
            try
            {
                var contents = await _manager.LanguageService.GetAllLanguagesAsync(false);
                return Ok(ApiResponse<IEnumerable<LanguageDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip language içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Language/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir language yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim languageu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Language", "Read")]
        public async Task<IActionResult> GetOneLanguageByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.LanguageService.GetLanguageByIdAsync(id, false);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen slug'a sahip language içerik yanıtlarını getirir
        /// </summary>
        /// <param name="slug">İçerik yanıtı slug'ı</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Language/GetSlug/customer-feedback-2024
        ///     
        ///     Bu endpoint belirtilen slug'a sahip içerik yanıtlarını getirir.
        ///     Örneğin:
        ///     - Belirli bir anketin tüm yanıtları
        ///     - Spesifik bir languageun tüm gönderileri
        ///     - Belirli bir dönemin geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("GetCode/{code}")]
        // [AuthorizePermission("Language", "Read")]
        public async Task<IActionResult> GetOneLanguageByCodeAsync([FromRoute] string code)
        {
            try
            {
                var content = await _manager.LanguageService.GetLanguageByCodeAsync(code, false);
                return Ok(ApiResponse<LanguageDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir language içerik yanıtı oluşturur
        /// </summary>
        /// <param name="language">Language verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="languageDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Language/Create
        ///     Content-Type: multipart/language-data
        ///     
        ///     {
        ///         "Text": {
        ///             "TR": "Müşteri geri bildirimi içeriği",
        ///             "EN": "Customer feedback content"
        ///         },
        ///         "Slug": {
        ///             "TR": "musteri-geri-bildirimi",
        ///             "EN": "customer-feedback"
        ///         },
        ///         "file": [binary_file_data] // Opsiyonel: Ek dosyalar
        ///     }
        ///     
        ///     Bu endpoint yeni bir içerik yanıtı oluşturur:
        ///     - Language gönderimleri
        ///     - Anket yanıtları
        ///     - İletişim mesajları
        ///     - Dosya ekleri ile birlikte geri bildirimler
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        [HttpPost("Create")]
        // [AuthorizePermission("Language", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        /// <summary>
        /// Mevcut bir language içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Language/Update
        ///     Content-Type: multipart/language-data
        ///     
        ///     {
        ///         "ID": 1,
        ///         "Text": {
        ///             "TR": "Güncellenmiş yanıt",
        ///             "EN": "Updated response"
        ///         },
        ///         "file": [binary_file_data] // Opsiyonel: Güncellenecek dosyalar
        ///     }
        ///     
        ///     Bu endpoint mevcut bir içerik yanıtını günceller:
        ///     - Language yanıtlarında düzeltme
        ///     - Anket cevaplarında güncelleme
        ///     - Geri bildirimlerde değişiklik
        ///     - Ek dosyaların güncellenmesi
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla güncellendi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Language", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        /// <summary>
        /// Belirtilen içerik yanıtını siler
        /// </summary>
        /// <param name="id">Silinecek içerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/Language/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını siler:
        ///     - Language yanıtlarının silinmesi
        ///     - Anket sonuçlarının kaldırılması
        ///     - Geri bildirimlerin temizlenmesi
        ///     
        ///     Dikkat: Bu işlem geri alınamaz ve ilişkili tüm dosyalar da silinir.
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla silindi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Language", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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