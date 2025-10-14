using System.Text.Json;
using Entities.DTOs.ProductDto;
using Entities.RequestFeature.Product;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// CMS product içeriklerinin yönetimi için controller (form yanıtları, anket sonuçları vb.)
    /// </summary>
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm product içerik yanıtlarını listeler
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Product/GetAll
        ///     
        ///     Bu endpoint sistemdeki tüm product içerik yanıtlarını listeler.
        ///     Örneğin:
        ///     - Form yanıtları
        ///     - Anket sonuçları
        ///     - İletişim formu mesajları
        ///     - Kullanıcı geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("GetAll")]
        // [AuthorizePermission("Product", "Read")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductParameters productParameters)
        {
            try
            {
                var products = await _manager.ProductService.GetAllProductsAsync(productParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(products.metaData));
                return Ok(ApiResponse<IEnumerable<ProductDto>>.CreateSuccess(_httpContextAccessor, products.productDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip product içerik yanıtını getirir
        /// </summary>
        /// <param name="id">İçerik yanıtı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Product/Get/1
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
        // [AuthorizePermission("Product", "Read")]
        public async Task<IActionResult> GetOneProductByIdAsync([FromRoute] int id, string lang)
        {
            try
            {
                var content = await _manager.ProductService.GetProductByIdAsync(id, lang, false);
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen slug'a sahip product içerik yanıtlarını getirir
        /// </summary>
        /// <param name="slug">İçerik yanıtı slug'ı</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/Product/GetSlug/customer-feedback-2024
        ///     
        ///     Bu endpoint belirtilen slug'a sahip içerik yanıtlarını getirir.
        ///     Örneğin:
        ///     - Belirli bir anketin tüm yanıtları
        ///     - Spesifik bir formun tüm gönderileri
        ///     - Belirli bir dönemin geri bildirimleri
        /// </remarks>
        /// <response code="200">İçerik yanıtları başarıyla getirildi</response>
        /// <response code="404">İçerik yanıtı bulunamadı</response>
        [HttpGet("GetSlug/{slug}")]
        // [AuthorizePermission("Product", "Read")]
        public async Task<IActionResult> GetOneProductBySlugAsync([FromRoute] string slug, string lang)
        {
            try
            {
                var content = await _manager.ProductService.GetProductBySlugAsync(slug, lang, false);
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni bir product içerik yanıtı oluşturur
        /// </summary>
        /// <param name="form">Form verisi (Text ve Slug için çoklu dil desteği)</param>
        /// <param name="productDtoForInsertion">İçerik yanıtı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Product/Create
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
        // [AuthorizePermission("Product", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOneProductAsync([FromBody] ProductDtoForInsertion productDtoForInsertion)
        {
            try
            {
                var content = await _manager.ProductService.CreateProductAsync(productDtoForInsertion);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Mevcut bir product içerik yanıtını günceller
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/Product/Update
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
        // [AuthorizePermission("Product", "Write")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneProductAsync([FromBody] ProductDtoForUpdate productDtoForUpdate)
        {
            try
            {
                var content = await _manager.ProductService.UpdateProductAsync(productDtoForUpdate);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
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
        ///     PUT /api/Product/Sort?id=1&amp;sort=2
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
        public async Task<IActionResult> SortProductAsync([FromRoute] int id, int sort)
        {
            try
            {
                var content = await _manager.ProductService.SortProductAsync(id, sort, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Updated"));
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
        ///     DELETE /api/Product/Delete/1
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
        // [AuthorizePermission("Product", "Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneProductAsync([FromRoute] int id)
        {
            try
            {
                var content = await _manager.ProductService.DeleteProductAsync(id, false);
                await _manager.VersioningService.UpdateVersioningAsync();
                return Ok(ApiResponse<ProductDto>.CreateSuccess(_httpContextAccessor, content, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}