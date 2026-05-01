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

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserParameters userParameters)
        {
            try
            {
                var users = await _manager.UserService.GetAllUsersAsync(userParameters, false);
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(users.metaData));
                return Ok(ApiResponse<IEnumerable<UserDto>>.CreateSuccess(_httpContextAccessor, users.userDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("GetAllUnActive")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllUnActiveUsersAsync([FromQuery] UserParameters userParameters)
        {
            try
            {
                var users = await _manager.UserService.GetAllUnActiveUsersAsync(userParameters, false);
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(users.metaData));
                return Ok(ApiResponse<IEnumerable<UserDto>>.CreateSuccess(_httpContextAccessor, users.userDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

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

        [HttpDelete("Delete/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOneUserAsync([FromRoute] string? userId)
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
