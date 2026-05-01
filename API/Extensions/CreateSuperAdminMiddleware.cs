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
                var superAdminFirstName = superAdminInfo["FirstName"];
                var superAdminLastName = superAdminInfo["LastName"];
                var superAdminPhoneNumber = superAdminInfo["PhoneNumber"];
                var superAdminUserName = superAdminInfo["UserName"];
                var superAdminEmail = superAdminInfo["Email"];
                var superAdminPassword = superAdminInfo["Password"];

                if (!string.IsNullOrEmpty(superAdminUserName))
                {
                    var superAdmin = await userManager.FindByNameAsync(superAdminUserName);
                    if (superAdmin == null && !string.IsNullOrEmpty(superAdminPassword))
                    {
                        superAdmin = new User
                        {
                            FirstName = superAdminFirstName,
                            LastName = superAdminLastName,
                            PhoneNumber = superAdminPhoneNumber,
                            UserName = superAdminUserName,
                            Email = superAdminEmail,
                            EmailConfirmed = true,
                            Active = true,
                            Roles = new List<IdentityRole> { new IdentityRole(superAdminRole) }
                        };
                        var result = await userManager.CreateAsync(superAdmin, superAdminPassword);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(superAdmin, superAdminRole);
                        }
                    }
                    else if (superAdmin != null && superAdmin.Active != true)
                    {
                        superAdmin.Active = true;
                        await userManager.UpdateAsync(superAdmin);
                    }
                    else if (superAdmin != null && (superAdmin.FirstName != superAdminFirstName || superAdmin.LastName != superAdminLastName || superAdmin.PhoneNumber != superAdminPhoneNumber || superAdmin.Email != superAdminEmail))
                    {
                        superAdmin.FirstName = superAdminFirstName;
                        superAdmin.LastName = superAdminLastName;
                        superAdmin.PhoneNumber = superAdminPhoneNumber;
                        superAdmin.Email = superAdminEmail;
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
