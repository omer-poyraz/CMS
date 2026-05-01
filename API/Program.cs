using Entities.Models;
using Microsoft.Extensions.FileProviders;
using API.Extensions;
using System.Text.Json.Serialization;
using Services.Extensions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

var emailSection = builder.Configuration.GetSection("EmailInfo");
builder.Services.AddScoped<Services.IMailService>(sp =>
    new Services.MailService(
        emailSection["Host"] ?? string.Empty,
        int.TryParse(emailSection["Port"], out var port) ? port : 587,
        emailSection["Email"] ?? string.Empty,
        emailSection["Password"] ?? string.Empty,
        emailSection["Email"] ?? string.Empty,
        null,
        bool.TryParse(emailSection["SSL"], out var ssl) ? ssl : true
    )
);

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(5134);
// });

builder.Services.AddHttpClient();

builder.Services.AddControllers(opt =>
    {
        opt.RespectBrowserAcceptHeader = true;
        opt.ReturnHttpNotAcceptable = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 128;
        options.JsonSerializerOptions.Converters.Add(new JsonDocumentConverter());
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    })
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json
            .ReferenceLoopHandling
            .Ignore;
        options.SerializerSettings.MaxDepth = 128;
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureGeneral();
builder.Services.ConfigureIdentity();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<GoogleAnalyticsOptions>(
    builder.Configuration.GetSection("GoogleAnalytics"));

var app = builder.Build();

app.UseHsts();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ContentPrettyFormatMiddleware>();
app.UseMiddleware<CreateSuperAdminMiddleware>();
app.UseMiddleware<LanguageMiddleware>();
app.UseMiddleware<LogMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot/images")
    ),
    RequestPath = "/images",
    ServeUnknownFileTypes = false,
    DefaultContentType = "image/jpeg",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=86400";
    }
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
