using System.Text.Json;
using Entities.DTOs.FilesDto;
using Entities.RequestFeature.Files;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS mediaUpload içeriklerinin yönetimi için controller (mediaUpload yanıtları, anket sonuçları vb.)
    /// </summary>
    [ApiController]
    [Route("api/File")]
    public class FileController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm mediaUpload içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Files/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm mediaUpload içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Files yanıtları
        ///     - Anket sonuçları
        ///     - İletişim mediaUploadu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Files", "Read")]
        public async Task<IActionResult> GetAllFilesAsync([FromQuery] FilesParameters filesParameters)
        {
            try
            {
                var files = await _manager.FilesService.GetAllFilesAsync(filesParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(files.metaData));
                return Ok(ApiResponse<IEnumerable<FilesDto>>.CreateSuccess(_httpContextAccessor, files.filesDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip mediaUpload içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Files/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir mediaUpload yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim mediaUploadu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("GetByFileType/{fileType}")]
        // [AuthorizePermission("Files", "Read")]
        public async Task<IActionResult> GetAllFilessFileTypeAsync([FromRoute] string fileType)
        {
            try
            {
                var content = await _manager.FilesService.GetAllFilessFileTypeAsync(fileType, false);
                return Ok(ApiResponse<IEnumerable<FilesDto>>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip mediaUpload içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Files/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir mediaUpload yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim mediaUploadu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("GetByWaterMarked")]
        // [AuthorizePermission("Files", "Read")]
        public async Task<IActionResult> GetAllFilessWaterMarkedsync([FromRoute] bool waterMarked)
        {
            try
            {
                var content = await _manager.FilesService.GetAllFilessWaterMarkedsync(waterMarked, false);
                return Ok(ApiResponse<IEnumerable<FilesDto>>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip mediaUpload içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Files/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını getirir.
        ///     Örneğin:
        ///     - Belirli bir mediaUpload yanıtının detayları
        ///     - Spesifik bir anket katılımcısının cevapları
        ///     - Tek bir iletişim mediaUploadu mesajı
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Files", "Read")]
        public async Task<IActionResult> GetOneFilesByIdAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.FilesService.GetFilesByIdAsync(id, false);
                return Ok(ApiResponse<FilesDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir mediaUpload içerik yanıtı oluşturur
        /// </summary>
        /// <param name="mediaUpload">Files verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="mediaUploadDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Files/Create
        ///     Content-Type: multipart/mediaUpload-data
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
        ///     - Files gönderimleri
        ///     - Anket yanıtları
        ///     - İletişim mesajları
        ///     - Dosya ekleri ile birlikte geri bildirimler
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        [HttpPost("Create")]
        // [AuthorizePermission("Files", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOneFilesAsync([FromForm] FilesDtoForInsertion mediaUploadDtoForInsertion)
        {
            try
            {
                if (mediaUploadDtoForInsertion.file != null)
                {
                    var rnd = new Random();
                    var imgId = rnd.Next(0, 100000);
                    var newList = new List<string>();
                    List<IFormFile> filesList = new List<IFormFile> { mediaUploadDtoForInsertion.file };
                    var uploadResults = await FileManager.FileUpload(filesList, imgId, "Media");
                    mediaUploadDtoForInsertion.FileUrl = uploadResults.FirstOrDefault()?["FilesFullPath"]?.ToString() ?? string.Empty;
                }
                var content = await _manager.FilesService.CreateFilesAsync(mediaUploadDtoForInsertion);
                return Ok(ApiResponse<FilesDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut bir mediaUpload içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Files/Update
        ///     Content-Type: multipart/mediaUpload-data
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
        ///     - Files yanıtlarında düzeltme
        ///     - Anket cevaplarında güncelleme
        ///     - Geri bildirimlerde değişiklik
        ///     - Ek dosyaların güncellenmesi
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla güncellendi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Files", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneFilesAsync([FromBody] FilesDtoForUpdate mediaUploadDtoForUpdate)
        {
            try
            {
                if (mediaUploadDtoForUpdate.file != null)
                {
                    var rnd = new Random();
                    var imgId = rnd.Next(0, 100000);
                    var newList = new List<string>();
                    List<IFormFile> filesList = new List<IFormFile> { mediaUploadDtoForUpdate.file };
                    var uploadResults = await FileManager.FileUpload(filesList, imgId, "Media");
                    mediaUploadDtoForUpdate.FileUrl = uploadResults.FirstOrDefault()?["FilesFullPath"]?.ToString() ?? string.Empty;
                }
                var content = await _manager.FilesService.UpdateFilesAsync(mediaUploadDtoForUpdate);
                return Ok(ApiResponse<FilesDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
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
        ///     DELETE /api/Files/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip içerik yanıtını siler:
        ///     - Files yanıtlarının silinmesi
        ///     - Anket sonuçlarının kaldırılması
        ///     - Geri bildirimlerin temizlenmesi
        ///     
        ///     Dikkat: Bu işlem geri alınamaz ve ilişkili tüm dosyalar da silinir.
        /// </remarks>
        /// <response code="200">İçerik yanıtı başarıyla silindi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Files", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneFilesAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.FilesService.DeleteFilesAsync(id, false);
                return Ok(ApiResponse<FilesDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}