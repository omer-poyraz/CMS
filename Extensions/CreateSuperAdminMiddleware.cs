using Microsoft.AspNetCore.Identity;
using Entities.Models;

namespace API.Extensions
{
    public class CreateSuperAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public CreateSuperAdminMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userManager = context.RequestServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var roleManager = context.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            var superAdminInfo = _configuration.GetSection("SuperAdminInfo");
            var superAdminRole = "Super Admin";

            if (roleManager != null && !await roleManager.RoleExistsAsync(superAdminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(superAdminRole));
            }

            if (userManager != null)
            {
                var superAdminUserName = superAdminInfo["UserName"];
                var superAdminEmail = superAdminInfo["Email"];
                var superAdminPassword = superAdminInfo["Password"];
                var superAdminPhone = superAdminInfo["PhoneNumber"];

                if (!string.IsNullOrEmpty(superAdminUserName))
                {
                    var superAdmin = await userManager.FindByNameAsync(superAdminUserName);
                    if (superAdmin == null && !string.IsNullOrEmpty(superAdminPassword))
                    {
                        superAdmin = new User
                        {
                            UserName = superAdminUserName,
                            Email = superAdminEmail,
                            EmailConfirmed = true,
                            PhoneNumber = superAdminPhone,
                            Active = true
                        };
                        var result = await userManager.CreateAsync(superAdmin, superAdminPassword);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(superAdmin, superAdminRole);
                        }
                    }
                    else if (superAdmin != null && superAdmin.Active != true)
                    {
                        // Update existing Super Admin to be active if not already
                        superAdmin.Active = true;
                        await userManager.UpdateAsync(superAdmin);
                    }
                }
            }

            await _next(context);
        }
    }

    public static class CreateSuperAdminMiddlewareExtensions
    {
        public static IApplicationBuilder UseCreateSuperAdmin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateSuperAdminMiddleware>();
        }
    }
}
