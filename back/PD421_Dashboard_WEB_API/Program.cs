using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PD421_Dashboard_WEB_API.BLL.Services.Auth;
using PD421_Dashboard_WEB_API.BLL.Services.EmailService;
using PD421_Dashboard_WEB_API.BLL.Services.Game;
using PD421_Dashboard_WEB_API.BLL.Services.Genre;
using PD421_Dashboard_WEB_API.BLL.Services.Storage;
using PD421_Dashboard_WEB_API.BLL.Settings;
using PD421_Dashboard_WEB_API.DAL;
using PD421_Dashboard_WEB_API.DAL.Entitites.Identity;
using PD421_Dashboard_WEB_API.DAL.Initializer;
using PD421_Dashboard_WEB_API.DAL.Repositories.Game;
using PD421_Dashboard_WEB_API.DAL.Repositories.Genre;
using PD421_Dashboard_WEB_API.Infrastructure;
using PD421_Dashboard_WEB_API.Middlewares;
using Serilog;
// 💡 Додаємо using для Azure Storage
using Azure.Storage.Blobs; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log_.txt", rollingInterval: RollingInterval.Hour)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDb"));
});

// Add identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg5NTE2ODAwIiwiaWF0IjoiMTc1ODAwNDY5MiIsImFjY291bnRfaWQiOiIwMTk5NTEzZTdlYmY3YjYwOGI4Y2I3NTI3YTE3ZTI5MyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazU4a3hoZXN2ZWI3aDZncms2MHBrYXJrIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.OMUeI0YxSQYUSUYehr5O6yevTWgsGamrSrCFSZ7Sd3fNsl01WU-pr6M6wusxNSxoQ6w8-lqrjOk6gj8KShQQhmvz91wRuRm_rObvAaDQEBRDit7iSUe6J7EH8lDmpqlUuJQ8zN0lCTgIDwaHDaI9h4FcSVy6qmi68oETGI876KCUf5ifCCwDSpZjirIws5XvO6IpQEkCp8FWd2UkTWvrHaaJWFbxOWfKbx_j5AeHPE1o5Piiz7qF6QKX8MzOj44f0yRExRKMCeQSauuRBgO33CooOm0mxbU2-Mx5tb3PPHdaFe7YxPKdRYSJ1TsRn3DELSrxnKsPE11X4eIXYuJh6w";
}, AppDomain.CurrentDomain.GetAssemblies());

// Add repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Add services
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGameService, GameService>();

// 💡 Налаштування Azure Blob Storage та реєстрація клієнта
var azureConnectionString = builder.Configuration.GetConnectionString("AzureStorage:ConnectionString");
var containerName = builder.Configuration.GetValue<string>("AzureStorage:ContainerName");

if (string.IsNullOrEmpty(azureConnectionString) || string.IsNullOrEmpty(containerName))
{
    // Важливо: Якщо конфігурація відсутня, кидаємо виняток або використовуємо заглушку.
    // Тут ми просто логуємо попередження і продовжуємо, але в реальному житті це може бути фатально.
    Log.Warning("Azure Storage Connection String or Container Name is missing in configuration. Storage operations will likely fail.");
}
else
{
    // Реєстрація BlobContainerClient як Singleton
    builder.Services.AddSingleton(x => 
    {
        var blobServiceClient = new BlobServiceClient(azureConnectionString);
        // Створюємо контейнер, якщо він ще не існує
        var container = blobServiceClient.GetBlobContainerClient(containerName);
        container.CreateIfNotExists(); 
        return container;
    });
}

// Реєстрація StorageService тепер залежить від BlobContainerClient, 
// який ми зареєстрували вище. Якщо DI не зможе його знайти, це може викликати помилку.
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// CORS
string corsName = "CorsAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsName, builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//  app.UseSwagger();
//  app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Custom middlewares
app.UseMiddleware<ExceptionHandleMiddleware>();
//app.UseMiddleware<LoggerMiddleware>();

// static files
app.AddStaticFiles(app.Environment);

app.UseCors(corsName);

app.UseAuthorization();

app.MapControllers();

app.Seed();

app.Run();
