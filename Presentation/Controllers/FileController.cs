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

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetAllFilesAsync([FromQuery] FilesParameters filesParameters)
        {
            try
            {
                var files = await _manager.FilesService.GetAllFilesAsync(filesParameters, false);
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(files.metaData));
                return Ok(ApiResponse<IEnumerable<FilesDto>>.CreateSuccess(_httpContextAccessor, files.filesDtos, "Success.Listed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
        }

        [HttpGet("GetByFileType/{fileType}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpGet("Get/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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

        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Super Admin")]
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