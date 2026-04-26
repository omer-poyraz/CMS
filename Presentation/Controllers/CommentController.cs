using Entities.DTOs.CommentDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS comment içeriklerinin yönetimi için controller (comment yanıtları, anket sonuçları vb.)
    /// </summary>
    [ApiController]
    [Route("api/Comment")]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm comment içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Comment/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm comment içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Comment yanıtları
        ///     - Anket sonuçları
        ///     - İletişim commentu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Comment", "Read")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            try
            {
                var contents = await _manager.CommentService.GetAllCommentsAsync(false);
                return Ok(ApiResponse<IEnumerable<CommentDto>>.CreateSuccess(_httpContextAccessor, contents, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip comment içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Comment/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir comment yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim commentu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Comment", "Read")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOneCommentByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.CommentService.GetCommentByIdAsync(id, false);
                return Ok(ApiResponse<CommentDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip comment içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Comment/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir comment yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim commentu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("GetByUser/{userId}")]
        // [AuthorizePermission("Comment", "Read")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOneCommentByUserIdAsync([FromRoute] string userId)
        {
            try
            {
                var content = await _manager.CommentService.GetCommentByUserIdAsync(userId, false);
                return Ok(ApiResponse<CommentDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir comment içerik yanıtı oluşturur
        /// </summary>
        /// <param name="comment">Comment verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="commentDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Comment/Create
        ///     Content-Type: multipart/comment-data
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
        ///     - Comment gönderimleri
        ///     - Anket yanıtları
        ///     - İletişim mesajları
        ///     - Dosya ekleri ile birlikte geri bildirimler
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        [HttpPost("Create")]
        // [AuthorizePermission("Comment", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOneCommentAsync([FromBody] CommentDtoForInsertion commentDtoForInsertion)
        {
            try
            {
                var content = await _manager.CommentService.CreateCommentAsync(commentDtoForInsertion);
                return Ok(ApiResponse<CommentDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut bir comment içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Comment/Update
        ///     Content-Type: multipart/comment-data
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
        ///     - Comment yanıtlarında düzeltme
        ///     - Anket cevaplarında güncelleme
        ///     - Geri bildirimlerde değişiklik
        ///     - Ek dosyaların güncellenmesi
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla güncellendi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Comment", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneCommentAsync([FromBody] CommentDtoForUpdate commentDtoForUpdate)
        {
            try
            {
                var content = await _manager.CommentService.UpdateCommentAsync(commentDtoForUpdate);
                return Ok(ApiResponse<CommentDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
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
        ///     DELETE /api/Comment/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını siler:
        ///     - Comment yanıtlarının silinmesi
        ///     - Anket sonuçlarının kaldırılması
        ///     - Geri bildirimlerin temizlenmesi
        ///     
        ///     Dikkat: Bu işlem geri alınamaz ve ilişkili tüm dosyalar da silinir.
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla silindi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Comment", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneCommentAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.CommentService.DeleteCommentAsync(id, false);
                return Ok(ApiResponse<CommentDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}