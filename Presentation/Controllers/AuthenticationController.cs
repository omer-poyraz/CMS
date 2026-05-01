using Entities.DTOs.UserDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<TokenDto>.CreateError(_httpContextAccessor, "Error.ValidationError", 400));

                if (!await _manager.AuthenticationService.ValidUser(userForAuthenticationDto))
                    return Unauthorized(ApiResponse<TokenDto>.CreateError(_httpContextAccessor, "Error.InvalidCredentials", 401));

                var token = await _manager.AuthenticationService.CreateToken(true);
                return Ok(ApiResponse<TokenDto>.CreateSuccess(_httpContextAccessor, token, "Success.LoginSuccess"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Register")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<bool>.CreateError(_httpContextAccessor, "Error.ValidationError", 400));

                await _manager.AuthenticationService.RegisterUser(userForRegisterDto);
                return Ok(ApiResponse<bool>.CreateSuccess(_httpContextAccessor, true, "Success.RegisterCodeSent"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            try
            {
                var tokenDtoToReturn = await _manager.AuthenticationService.RefreshToken(tokenDto);
                return Ok(ApiResponse<TokenDto>.CreateSuccess(_httpContextAccessor, tokenDtoToReturn, "Success.TokenRefreshed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }
    }
}
