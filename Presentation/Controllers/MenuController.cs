using System.Text.Json;
using Entities.DTOs.MenuDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS menu içeriklerinin yönetimi için controller (form yanıtları, anket sonuçları vb.)
    /// </summary>
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

        /// <summary>
        /// Tüm menu içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Menu/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm menu içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Menu", "Read")]
        public async Task<IActionResult> GetAllMenusAsync()
        {
            try
            {
                var contents = await _manager.MenuService.GetAllMenusAsync(false);
                return Ok(ApiResponse<IEnumerable<MenuDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<IEnumerable<MenuDto>>.CreateError(_httpContextAccessor, "Error.ServerError"));
            }
        }

        /// <summary>
        /// Tüm menu içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Menu/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm menu içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAllByGroup/{id:int}")]
        // [AuthorizePermission("Menu", "Read")]
        public async Task<IActionResult> GetAllMenusAsync([FromRoute] int id)
        {
            try
            {
                var contents = await _manager.MenuService.GetAllMenusByGroupAsync(id, false);
                return Ok(ApiResponse<IEnumerable<MenuDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<IEnumerable<MenuDto>>.CreateError(_httpContextAccessor, "Error.ServerError"));
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip menu içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Menu/Get/1
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
        // [AuthorizePermission("Menu", "Read")]
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

        /// <summary>
        /// Yeni bir menu içerik yanıtı oluşturur
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="menuDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Menu/Create
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
        // [AuthorizePermission("Menu", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOneMenuAsync([FromBody] MenuDtoForInsertion menuDtoForInsertion)
        {
            try
            {
                var content = await _manager.MenuService.CreateMenuAsync(menuDtoForInsertion);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception)
            {
                return BadRequest(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.ServerError", 400));
            }
        }

        /// <summary>
        /// Mevcut bir menu içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Menu/Update
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
        // [AuthorizePermission("Menu", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneMenuAsync([FromBody] MenuDtoForUpdate menuDtoForUpdate)
        {
            try
            {
                var content = await _manager.MenuService.UpdateMenuAsync(menuDtoForUpdate);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.NotFound", 404));
            }
        }

        /// <summary>
        /// İçerik yanıtlarının sıralama önceliğini günceller
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <param name="sort">Yeni sıra numarası</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Menu/Sort?id=1&amp;sort=2
        ///     
        ///     Bu endpoint içerik yanıtlarının görüntülenme sırasını değiştirir:
        ///     - Anket sonuçlarının sıralaması
        ///     - Form yanıtlarının öncelik sırası
        ///     - Geri bildirimlerin görüntülenme sırası
        /// </remarks>
        /// <response code="200">Sıralama başarıyla güncellendi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpPut("Sort/{id:int}/{sort:int}")]
        // [AuthorizePermission("Module", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SortMenuAsync([FromRoute] int id, int sort)
        {
            try
            {
                var content = await _manager.MenuService.SortMenuAsync(id, sort, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
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
        ///     DELETE /api/Menu/Delete/1
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
        // [AuthorizePermission("Menu", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneMenuAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.MenuService.DeleteMenuAsync(id, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<MenuDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse<MenuDto>.CreateError(_httpContextAccessor, "Error.NotFound", 404));
            }
        }
    }
}