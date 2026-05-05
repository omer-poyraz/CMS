using System.Reflection;
using System.Text;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using Services.Extensions;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
        }

        public static void ConfigureGeneral(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddSingleton(new FileReaderService());
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IFilesRepository, FilesRepository>();
            services.AddScoped<IFilesService, FilesService>();

            services.AddScoped<IGoogleAnalyticsRepository, GoogleAnalyticsRepository>();
            services.AddScoped<IGoogleAnalyticsService, GoogleAnalyticsService>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageService, LanguageService>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IMailTemplateService, MailTemplateService>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddScoped<IMenuGroupRepository, MenuGroupRepository>();
            services.AddScoped<IMenuGroupService, MenuGroupService>();

            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPageService, PageService>();

            services.AddScoped<IPopupRepository, PopupRepository>();
            services.AddScoped<IPopupService, PopupService>();

            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<ISettingsService, SettingsService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddRateLimiter(_ =>
            {
                _.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 30;
                    options.Window = TimeSpan.FromMinutes(1);
                });
            });


            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    build =>
                        build
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromHours(1))
                            .WithExposedHeaders("X-Pagination")
                            .WithOrigins(
                                "http://localhost:3000"
                            )
                );
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 10;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidAudience = jwtSettings["validAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = 401;
                            context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:3000";

                            await context.Response.WriteAsync("Unauthorized");
                        },
                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:3000";

                            await context.Response.WriteAsync("Forbidden");
                        }
                    };

                });
        }

        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerInfo = configuration.GetSection("SwaggerInfo");

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = swaggerInfo["Title"], Version = swaggerInfo["Version"] });
                s.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Description = "Bearer xxTOKENxx",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                    }
                );

                s.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Name = "Bearer",
                    },
                    new List<string>()
                },
                    }
                );

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                var presentationXmlFile = "Presentation.xml";
                var presentationXmlPath = Path.Combine(AppContext.BaseDirectory, presentationXmlFile);

                if (File.Exists(apiXmlPath))
                {
                    s.IncludeXmlComments(apiXmlPath);
                }

                if (File.Exists(presentationXmlPath))
                {
                    s.IncludeXmlComments(presentationXmlPath);
                }
            });
        }
    }
}
