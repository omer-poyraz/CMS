// using Entities.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.Extensions.DependencyInjection;
// using Services.Contracts;

// namespace Services.Extensions
// {
//     public class AuthorizePermissionAttribute : Attribute, IAsyncAuthorizationFilter
//     {
//         private readonly string _serviceName;
//         private readonly string _permissionType;

//         public AuthorizePermissionAttribute(string serviceName, string permissionType)
//         {
//             _serviceName = serviceName;
//             _permissionType = permissionType;
//         }

//         public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//         {
//             var userManager = context.HttpContext.RequestServices.GetService<UserManager<User>>();
//             if (userManager == null)
//             {
//                 context.Result = new ObjectResult(
//                     new { statusCode = 500, message = "Kullanıcı yönetim servisi bulunamadı." }
//                 )
//                 {
//                     StatusCode = 500,
//                 };
//                 return;
//             }

//             var userName = context.HttpContext.User.Identity?.Name;

//             if (string.IsNullOrEmpty(userName))
//             {
//                 context.Result = new ObjectResult(
//                     new { statusCode = 403, message = "Yetkiniz yok!" }
//                 )
//                 {
//                     StatusCode = 403,
//                 };
//                 return;
//             }

//             var user = await userManager.FindByNameAsync(userName);
//             var userId = user?.Id;

//             var userRoles = await userManager.GetRolesAsync(user!);
//             if (userRoles.Contains("Super Admin"))
//             {
//                 return;
//             }

//             if (string.IsNullOrEmpty(userId))
//             {
//                 context.Result = new ObjectResult(
//                     new { statusCode = 403, message = "Yetkiniz yok!" }
//                 )
//                 {
//                     StatusCode = 403,
//                 };
//                 return;
//             }

//             var userPermissionService =
//                 context.HttpContext.RequestServices.GetService<IUserPermissionService>();

//             if (userPermissionService == null)
//             {
//                 context.Result = new ObjectResult(
//                     new { statusCode = 500, message = "Yetki servisi bulunamadı." }
//                 )
//                 {
//                     StatusCode = 500,
//                 };
//                 return;
//             }

//             var hasPermission = await userPermissionService.HasPermissionAsync(
//                 userId,
//                 _serviceName,
//                 _permissionType
//             );

//             if (!hasPermission)
//             {
//                 context.Result = new ObjectResult(
//                     new { statusCode = 403, message = "Yetkiniz yok!" }
//                 )
//                 {
//                     StatusCode = 403,
//                 };
//             }
//         }
//     }
// }
