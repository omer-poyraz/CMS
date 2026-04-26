using Entities.DTOs.MenuGroupDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS menuGroup içeriklerinin yönetimi için controller (form yanıtları, anket sonuçları vb.)
    /// </summary>
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

        /// <summary>
        /// Tüm menuGroup içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/MenuGroup/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm menuGroup içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
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

        /// <summary>
        /// Belirtilen ID'ye sahip menuGroup içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/MenuGroup/Get/1
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

        /// <summary>
        /// Yeni bir menuGroup içerik yanıtı oluşturur
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="menuGroupDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/MenuGroup/Create
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

        /// <summary>
        /// Mevcut bir menuGroup içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/MenuGroup/Update
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

        /// <summary>
        /// Belirtilen içerik yanıtını siler
        /// </summary>
        /// <param name="id">Silinecek içerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/MenuGroup/Delete/1
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