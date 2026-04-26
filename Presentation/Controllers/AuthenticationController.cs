using Entities.DTOs.UserDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    /// <summary>
    /// Authentication işlemleri için controller
    /// </summary>
    [ApiController]
    [Route("api/Authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        /// <param name="userForAuthenticationDto">Kullanıcı giriş bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Authentication/Login
        ///     {
        ///         "userName": "test_user",
        ///         "password": "Test123!"
        ///     }
        ///     
        ///     Swagger üzerinden çalışma yapılacaksa token yukarıda bulunan "Authorize" butonuna tıklanarak input içerisine "Bearer {token}" şeklinde yazılmalıdır.
        ///     
        ///     Frontend tarafında çalışma yapılacaksa accessToken parametresi alınarak diğer servislere atılan isteklerde header kısmında "Authorization": "Bearer {accessToken}" şeklinde gönderilmelidir.
        /// </remarks>
        /// <response code="200">Başarılı giriş</response>
        /// <response code="400">Geçersiz giriş bilgileri</response>
        /// <response code="401">Yetkisiz erişim</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<TokenDto>.CreateError(
                        _httpContextAccessor,
                        "Error.ValidationError",
                        400));

                if (!await _manager.AuthenticationService.ValidUser(userForAuthenticationDto))
                    return Unauthorized(ApiResponse<TokenDto>.CreateError(
                        _httpContextAccessor,
                        "Error.InvalidCredentials",
                        401));

                var token = await _manager.AuthenticationService.CreateToken(true);
                return Ok(ApiResponse<TokenDto>.CreateSuccess(
                    _httpContextAccessor,
                    token,
                    "Success.LoginSuccess"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Yeni kullanıcı kaydı oluşturur
        /// </summary>
        /// <param name="userForRegisterDto">Kullanıcı kayıt bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Authentication/Register
        ///     {
        ///         "firstName": "Test",
        ///         "lastName": "User",
        ///         "userName": "test_user",
        ///         "email": "test@example.com",
        ///         "password": "Test123!",
        ///         "firstName": "string",
        ///         "gender": "Kadın",
        ///         "company": "Liberyus",
        ///         "phone2": "05555555555",
        ///         "fax": "05555555555",
        ///         "address": "Reşit galip caddesi no: 1",
        ///         "phoneNumber": "+901234567890",
        ///         "roles": ["User"]
        ///     }
        ///     
        ///     Super Admin 1 kişi olabilir.
        ///     Roles alanına bir rol girmek zorunludur. (Admin, User)
        ///     Email alanı benzersiz olmalıdır.
        ///     Şifre en az 6 karakter olmalıdır. (Büyük harf, küçük harf, rakam ve sembol içermelidir.)
        /// </remarks>
        /// <response code="200">Kayıt başarılı</response>
        /// <response code="400">Geçersiz kayıt bilgileri veya validasyon hatası</response>
        /// <response code="500">Sunucu hatası</response>

        /// <summary>
        /// Kayıt için doğrulama kodu gönderir
        /// </summary>
        // [HttpPost("RegisterRequest")]
        // public async Task<IActionResult> RegisterRequest([FromBody] UserForRegisterDto userForRegisterDto)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid)
        //             return BadRequest(ApiResponse<bool>.CreateError(
        //                 _httpContextAccessor,
        //                 "Error.ValidationError",
        //                 400));

        //         await _manager.AuthenticationService.RegisterRequest(userForRegisterDto);
        //         return Ok(ApiResponse<bool>.CreateSuccess(
        //             _httpContextAccessor,
        //             true,
        //             "Success.RegisterCodeSent"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }

        [HttpPost("Register")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<bool>.CreateError(
                        _httpContextAccessor,
                        "Error.ValidationError",
                        400));

                await _manager.AuthenticationService.RegisterUser(userForRegisterDto);
                return Ok(ApiResponse<bool>.CreateSuccess(
                    _httpContextAccessor,
                    true,
                    "Success.RegisterCodeSent"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        /// <summary>
        /// Doğrulama kodu ile kullanıcı kaydını tamamlar
        /// </summary>
        // [HttpPost("RegisterConfirm")]
        // public async Task<IActionResult> RegisterConfirm([FromBody] RegisterConfirmRequest request)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid)
        //             return BadRequest(ApiResponse<IdentityResult>.CreateError(
        //                 _httpContextAccessor,
        //                 "Error.ValidationError",
        //                 400));

        //         var result = await _manager.AuthenticationService.RegisterConfirm(request.User, request.Code);

        //         if (!result.Succeeded)
        //         {
        //             var errors = result.Errors.Select(e => e.Description).ToList();
        //             foreach (var error in result.Errors)
        //             {
        //                 ModelState.TryAddModelError(error.Code, error.Description);
        //             }
        //             return BadRequest(ApiResponse<List<string>>.CreateError(
        //                 _httpContextAccessor,
        //                 "Error.RegisterFailed",
        //                 400));
        //         }

        //         return Ok(ApiResponse<IdentityResult>.CreateSuccess(
        //             _httpContextAccessor,
        //             result,
        //             "Success.RegisterSuccess"));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { statusCode = 400, message = ex.Message });
        //     }
        // }
        // RegisterConfirm için yardımcı model
        public class RegisterConfirmRequest
        {
            public UserForRegisterDto User { get; set; }
            public string Code { get; set; }
        }

        /// <summary>
        /// Access token'ı yeniler
        /// </summary>
        /// <param name="tokenDto">Yenilenecek token bilgileri</param>
        /// <remarks>
        /// Örnek istek:
        /// 
        ///     POST /api/Authentication/Refresh
        ///     {
        ///         "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
        ///         "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
        ///     }
        ///     
        ///     Access token süresi dolduğunda bu endpoint kullanılarak yeni bir access token alınabilir.
        ///     Refresh token süresi dolmamış olmalıdır.
        ///     Refresh token bir kez kullanıldıktan sonra geçersiz olur ve yeni bir refresh token üretilir.
        /// </remarks>
        /// <response code="200">Token başarıyla yenilendi</response>
        /// <response code="400">Geçersiz token</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            try
            {
                var tokenDtoToReturn = await _manager.AuthenticationService.RefreshToken(tokenDto);
                return Ok(ApiResponse<TokenDto>.CreateSuccess(
                    _httpContextAccessor,
                    tokenDtoToReturn,
                    "Success.TokenRefreshed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
