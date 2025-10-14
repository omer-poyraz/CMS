using Entities.DTOs.VideoDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS video içeriklerinin yönetimi için controller (form yanıtları, anket sonuçları vb.)
    /// </summary>
    [ApiController]
    [Route("api/Video")]
    public class VideoController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm video içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Video/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm video içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Video", "Read")]
        public async Task<IActionResult> GetAllVideosAsync(string lang = "tr")
        {
            try
            {
                var contents = await _manager.VideoService.GetAllVideosAsync(lang, false);
                return Ok(ApiResponse<IEnumerable<VideoDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Tüm video içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Video/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm video içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAllByGroup/{videoGroupId:int}")]
        // [AuthorizePermission("Video", "Read")]
        public async Task<IActionResult> GetAllVideosByGroupAsync([FromRoute] int videoGroupId, string lang)
        {
            try
            {
                var contents = await _manager.VideoService.GetAllVideosByVideoGroupIdAsync(videoGroupId, lang, false);
                return Ok(ApiResponse<IEnumerable<VideoDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip video içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Video/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir form yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim formu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Video", "Read")]
        public async Task<IActionResult> GetOneVideoByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.VideoService.GetVideoByIdAsync(id, false);
                return Ok(ApiResponse<VideoDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir video içerik yanıtı oluşturur
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="videoDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Video/Create
        ///     Content-Type: multipart/form-data
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
        ///     - Form gönderimleri
        ///     - Anket yanıtları
        ///     - İletişim mesajları
        ///     - Dosya ekleri ile birlikte geri bildirimler
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        [HttpPost("Create")]
        // [AuthorizePermission("Video", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOneVideoAsync([FromBody] VideoDtoForInsertion videoDtoForInsertion)
        {
            try
            {
                var content = await _manager.VideoService.CreateVideoAsync(videoDtoForInsertion);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<VideoDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut bir video içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Video/Update
        ///     Content-Type: multipart/form-data
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
        ///     - Form yanıtlarında düzeltme
        ///     - Anket cevaplarında güncelleme
        ///     - Geri bildirimlerde değişiklik
        ///     - Ek dosyaların güncellenmesi
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla güncellendi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Video", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneVideoAsync([FromBody] VideoDtoForUpdate videoDtoForUpdate)
        {
            try
            {
                var content = await _manager.VideoService.UpdateVideoAsync(videoDtoForUpdate);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<VideoDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
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
        ///     DELETE /api/Video/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını siler:
        ///     - Form yanıtlarının silinmesi
        ///     - Anket sonuçlarının kaldırılması
        ///     - Geri bildirimlerin temizlenmesi
        ///     
        ///     Dikkat: Bu işlem geri alınamaz ve ilişkili tüm dosyalar da silinir.
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla silindi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Video", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneVideoAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.VideoService.DeleteVideoAsync(id, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<VideoDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}