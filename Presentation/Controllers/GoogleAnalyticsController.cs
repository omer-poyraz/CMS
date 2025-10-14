// using Entities.DTOs.GoogleAnalyticsDto;
// using Entities.Response;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Services.Contracts;
// using Services.Extensions;

// namespace Presentation.Controllers
// {
//     [ApiController]
//     [Route("api/GoogleAnalytics")]
//     public class GoogleAnalyticsController : ControllerBase
//     {
//         private readonly IServiceManager _manager;
//         private readonly IHttpContextAccessor _httpContextAccessor;

//         public GoogleAnalyticsController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
//         {
//             _manager = manager;
//             _httpContextAccessor = httpContextAccessor;
//         }

//         /// <summary>
//         /// Google Analytics verilerini rapor olarak getirir
//         /// </summary>
//         [HttpPost("GetReport")]
//         // [AuthorizePermission("GoogleAnalytics", "Read")]
//         [Authorize(AuthenticationSchemes = "Bearer")]
//         public async Task<IActionResult> GetReportAsync([FromBody] AnalyticsReportRequest request)
//         {
//             try
//             {
//                 var report = await _manager.GoogleAnalyticsService.GetAnalyticsReportGA4Async(request);
//                 return Ok(ApiResponse<AnalyticsReportResponse>.CreateSuccess(_httpContextAccessor, report, "Success.Retrieved"));
//             }
//             catch (Exception ex)
//             {
//                 return BadRequest(new { statusCode = 400, message = ex.Message });
//             }
//         }

//         /// <summary>
//         /// Google Analytics özet verilerini getirir
//         /// </summary>
//         [HttpGet("GetSummary")]
//         // [AuthorizePermission("GoogleAnalytics", "Read")]
//         [Authorize(AuthenticationSchemes = "Bearer")]
//         public async Task<IActionResult> GetSummaryAsync([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
//         {
//             try
//             {
//                 var summary = await _manager.GoogleAnalyticsService.GetAnalyticsSummaryAsync(startDate, endDate);
//                 return Ok(ApiResponse<AnalyticsSummary>.CreateSuccess(_httpContextAccessor, summary, "Success.Retrieved"));
//             }
//             catch (Exception ex)
//             {
//                 return BadRequest(new { statusCode = 400, message = ex.Message });
//             }
//         }
//     }
// }