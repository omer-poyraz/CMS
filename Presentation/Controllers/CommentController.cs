using Entities.DTOs.CommentDto;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
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

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpGet("GetByUser/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpPost("Create")]
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

        [HttpPut("Update")]
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

        [HttpDelete("Delete/{id:int}")]
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