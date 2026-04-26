using System.Text.Json;
using Entities.DTOs.UserDto;
using Entities.RequestFeature.User;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    /// <summary>
    /// Kullanıcı yönetimi için controller
    /// </summary>
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Tüm kullanıcıları listeler
        /// </summary>
        /// <param name="userParameters">Sayfalama ve filtreleme parametreleri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/User/GetAll?PageNumber=1&amp;PageSize=10
        ///     
        ///     Bu endpoint sistemdeki tüm kullanıcıları listeler.
        ///     Sayfalama ve filtreleme özellikleri:
        ///     - PageNumber: Sayfa numarası
        ///     - PageSize: Sayfa başına kullanıcı sayısı
        ///     - SearchTerm: Arama terimi (isim, email vb.)
        ///     - OrderBy: Sıralama kriteri
        /// </remarks>
        /// <response code="200">Kullanıcılar başarıyla listelendi</response>
        /// <response code="400">İşlem başarısız</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserParameters userParameters)
        {
            try
            {
                var users = await _manager.UserService.GetAllUsersAsync(userParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.metaData));
                return Ok(ApiResponse<IEnumerable<UserDto>>.CreateSuccess(_httpContextAccessor, users.userDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Tüm kullanıcıları listeler
        /// </summary>
        /// <param name="userParameters">Sayfalama ve filtreleme parametreleri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/User/GetAll?PageNumber=1&amp;PageSize=10
        ///     
        ///     Bu endpoint sistemdeki tüm kullanıcıları listeler.
        ///     Sayfalama ve filtreleme özellikleri:
        ///     - PageNumber: Sayfa numarası
        ///     - PageSize: Sayfa başına kullanıcı sayısı
        ///     - SearchTerm: Arama terimi (isim, email vb.)
        ///     - OrderBy: Sıralama kriteri
        /// </remarks>
        /// <response code="200">Kullanıcılar başarıyla listelendi</response>
        /// <response code="400">İşlem başarısız</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpGet("GetAllUnActive")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllUnActiveUsersAsync([FromQuery] UserParameters userParameters)
        {
            try
            {
                var users = await _manager.UserService.GetAllUnActiveUsersAsync(userParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.metaData));
                return Ok(ApiResponse<IEnumerable<UserDto>>.CreateSuccess(_httpContextAccessor, users.userDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip kullanıcıyı getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/User/Get/{userId}
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip kullanıcının detaylarını getirir:
        ///     - Kişisel bilgiler
        ///     - İletişim bilgileri
        ///     - Rol ve yetkiler
        ///     - Hesap durumu
        /// </remarks>
        /// <response code="200">Kullanıcı başarıyla getirildi</response>
        /// <response code="400">Kullanıcı bulunamadı</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpGet("Get/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOneUserByIdAsync([FromRoute] string? userId)
        {
            try
            {
                var user = await _manager.UserService.GetOneUserByIdAsync(userId, false);
                return Ok(ApiResponse<UserDto>.CreateSuccess(_httpContextAccessor, user, "Success.Retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı bilgilerini günceller
        /// </summary>
        /// <param name="userDtoForUpdate">Güncellenecek kullanıcı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/User/Update
        ///     {
        ///         "userId": "abc123",
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "email": "john.doe@example.com",
        ///         "phoneNumber": "+90555123456",
        ///         "roles": ["Admin"]
        ///     }
        ///     
        ///     Güncellenebilen bilgiler:
        ///     - Kişisel bilgiler
        ///     - İletişim bilgileri
        ///     - Rol ve yetkiler
        /// </remarks>
        /// <response code="200">Kullanıcı başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateOneUserAsync([FromBody] UserDtoForUpdate userDtoForUpdate
        )
        {
            try
            {
                var user = await _manager.UserService.UpdateOneUserAsync(userDtoForUpdate.UserId, userDtoForUpdate, false);
                return Ok(ApiResponse<UserDto>.CreateSuccess(_httpContextAccessor, user, "Success.Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı bilgilerini günceller
        /// </summary>
        /// <param name="userDtoForUpdate">Güncellenecek kullanıcı bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/User/Update
        ///     {
        ///         "userId": "abc123",
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "email": "john.doe@example.com",
        ///         "phoneNumber": "+90555123456",
        ///         "roles": ["Admin"]
        ///     }
        ///     
        ///     Güncellenebilen bilgiler:
        ///     - Kişisel bilgiler
        ///     - İletişim bilgileri
        ///     - Rol ve yetkiler
        /// </remarks>
        /// <response code="200">Kullanıcı başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpPut("UserActivation")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> UserActivationAsync(string userId)
        {
            try
            {
                var user = await _manager.UserService.UserActivationAsync(userId, false);
                return Ok(ApiResponse<UserDto>.CreateSuccess(_httpContextAccessor, user, "Success.Activated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı hesabını siler
        /// </summary>
        /// <param name="userId">Silinecek kullanıcı ID'si</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     DELETE /api/User/Delete/{userId}
        ///     
        ///     Bu endpoint belirtilen ID'ye sahip kullanıcıyı sistemden siler.
        ///     Dikkat:
        ///     - Silinen kullanıcı geri alınamaz
        ///     - Kullanıcıya ait tüm veriler silinir
        ///     - İlişkili kayıtlar etkilenebilir
        /// </remarks>
        /// <response code="200">Kullanıcı başarıyla silindi</response>
        /// <response code="400">Kullanıcı bulunamadı</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpDelete("Delete/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneUserAsync([FromQuery] string? userId)
        {
            try
            {
                var user = await _manager.UserService.DeleteOneUserAsync(userId, false);
                return Ok(ApiResponse<UserDto>.CreateSuccess(_httpContextAccessor, user, "Success.Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı şifresini değiştirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <param name="changePassword">Şifre değiştirme bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     PUT /api/User/ChangePassword/{userId}
        ///     {
        ///         "currentPassword": "OldPass123!",
        ///         "newPassword": "NewPass123!"
        ///     }
        ///     
        ///     Güvenlik gereksinimleri:
        ///     - Mevcut şifre doğrulanır
        ///     - Yeni şifre politikaya uygun olmalıdır
        ///     - Önceki şifrelerden farklı olmalıdır
        /// </remarks>
        /// <response code="200">Şifre başarıyla değiştirildi</response>
        /// <response code="400">Geçersiz şifre veya kullanıcı bulunamadı</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpPut("ChangePassword/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePaswordAsync([FromRoute] string? userId, [FromBody] UserDtoForChangePassword changePassword)
        {
            try
            {
                var user = await _manager.UserService.ChangePasswordAsync(userId, changePassword.CurrentPassword!, changePassword.NewPassword!, false);
                return Ok(ApiResponse<UserDto>.CreateSuccess(_httpContextAccessor, user, "Success.PasswordChanged"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı şifresini sıfırlamak için e-posta gönderir
        /// </summary>
        /// <param name="mail">Kullanıcının e-posta adresi</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     GET /api/User/ResetPassword/user@example.com
        ///     
        ///     Kullanıcıya şifre sıfırlama bağlantısı içeren bir e-posta gönderir.
        /// </remarks>
        /// <response code="200">Sıfırlama e-postası başarıyla gönderildi</response>
        /// <response code="400">Kullanıcı bulunamadı veya e-posta gönderilemedi</response>
        [HttpGet("ResetPassword/{mail}")]
        public async Task<IActionResult> ResetPasswordAsync([FromRoute] string? mail)
        {
            try
            {
                var result = await _manager.UserService.ResetPasswordAsync(mail);
                return Ok(ApiResponse<bool>.CreateSuccess(_httpContextAccessor, result, "Success.PasswordReset"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
