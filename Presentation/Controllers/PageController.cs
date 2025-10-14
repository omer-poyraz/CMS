using System.Text.Json;
using Entities.DTOs.PageDto;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;
using Entities.RequestFeature.Page;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Page")]
    public class PageController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PageController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm sayfa içeriklerini listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Page/GetAll
        ///     
        ///     Bu endpoint CMS'teki tüm sayfa içeriklerini (header, footer, homepage vb.) listeler.
        ///     Yetkilendirme gerektirmez.
        /// </remarks>
        /// <response code="200">Sayfa içerikleri başarıyla listelendi</response>
        /// <response code="401">Yetkisiz erişim</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Page", "Read")]
        public async Task<IActionResult> GetAllPagesAsync([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var users = await _manager.PageService.GetAllPagesAsync(pageParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.metaData));
                return Ok(ApiResponse<IEnumerable<PageDto>>.CreateSuccess(_httpContextAccessor, users.pageDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip sayfa içeriğini getirir
        /// </summary>
        /// <param name="id">Sayfa içeriği ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Page/Get/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip sayfa içeriğini getirir.
        ///     Örneğin: Header, footer veya anasayfa içeriği.
        /// </remarks>
        /// <response code="200">Sayfa içeriği başarıyla getirildi</response>
        /// <response code="404">Sayfa içeriği bulunamadı</response>
        [HttpGet("Get/{id:int}")]
        // [AuthorizePermission("Page", "Read")]
        public async Task<IActionResult> GetOnePageByIdAsync([FromRoute] int id)
        {
            try
            {
                var user = await _manager.PageService.GetPageByIdAsync(id, false);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen URL slug'ına sahip sayfa içeriğini getirir. Slug belirtilmezse anasayfayı döndürür.
        /// </summary>
        [HttpGet("GetSlug/{*slug}")]
        public async Task<IActionResult> GetOnePageBySlugAsync(string slug, string lang)
        {
            try
            {
                var page = await _manager.PageService.GetPageBySlugAsync(slug, lang, false);
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, page, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir sayfa içeriği oluşturur
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="pageContentDtoForInsertion">Sayfa içeriği bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Page/Create
        ///     Content-Type: multipart/form-data
        ///     
        ///     {
        ///         "Title": {
        ///             "TR": "Hakkımızda",
        ///             "EN": "About us"
        ///         },
        ///         "Slug": {
        ///             "TR": "hakkimizda",
        ///             "EN": "about-us"
        ///         },
        ///         "Description": {
        ///             "TR": "Hakkımızda sayfası",
        ///             "EN": "About us page"
        ///         },
        ///         "Content": {
        ///             "TR": "Hakkımızda sayfası içeriği",
        ///             "EN": "About us page content"
        ///         },
        ///         "file": [binary_file_data]
        ///     }
        ///     
        ///     Bu endpoint yeni bir CMS sayfa içeriği oluşturur (örn: yeni bir sayfa).
        ///     Çoklu dil desteği ve dosya yükleme özelliği vardır.
        /// </remarks>
        /// <response code="200">Sayfa içeriği başarıyla oluşturuldu</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="400">Geçersiz veri</response>
        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> CreateOnePageAsync([FromBody] PageDtoForInsertion pageDtoForInsertion)
        {
            try
            {
                var user = await _manager.PageService.CreatePageAsync(pageDtoForInsertion);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, user, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut bir sayfa içeriğini günceller
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="pageContentDtoForUpdate">Güncellenecek sayfa içeriği bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Page/Update
        ///     Content-Type: multipart/form-data
        ///     
        ///     {
        ///         "ID": 1,
        ///         "Title": {
        ///             "TR": "Hakkımızda",
        ///             "EN": "About us"
        ///         },
        ///         "Slug": {
        ///             "TR": "guncel-sayfa",
        ///             "EN": "updated-page"
        ///         },
        ///         "Description": {
        ///             "TR": "Güncellenmiş sayfa",
        ///             "EN": "Updated page"
        ///         },
        ///         "Content": {
        ///             "TR": "Güncellenmiş sayfa içeriği",
        ///             "EN": "Updated page content"
        ///         },
        ///         "file": [binary_file_data]
        ///     }
        ///     
        ///     Bu endpoint mevcut bir CMS sayfa içeriğini günceller.
        ///     Çoklu dil desteği ve dosya güncelleme özelliği vardır.
        /// </remarks>
        /// <response code="200">Sayfa içeriği başarıyla güncellendi</response>
        /// <response code="403">Yetersiz yetki</response>
        /// <response code="404">Sayfa içeriği bulunamadı</response>
        [HttpPut("Update")]
        // [AuthorizePermission("Page", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneUserAsync([FromBody] PageDtoForUpdate pageDtoForUpdate)
        {
            try
            {
                var user = await _manager.PageService.UpdatePageAsync(pageDtoForUpdate);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, user, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen sayfa içeriğini siler
        /// </summary>
        /// <param name="id">Silinecek sayfa içeriği ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/Page/Delete/1
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip sayfa içeriğini siler.
        ///     Silinen içerik geri alınamaz.
        /// </remarks>
        /// <response code="200">Sayfa içeriği başarıyla silindi</response>
        /// <response code="403">Yetkisiz yetki</response>
        /// <response code="404">Sayfa içeriği bulunamadı</response>
        [HttpDelete("Delete/{id:int}")]
        // [AuthorizePermission("Page", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOnePageAsync([FromRoute] int id)
        {
            try
            {
                var user = await _manager.PageService.DeletePageAsync(id, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<PageDto>.CreateSuccess(_httpContextAccessor, user, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
