using FormulariosUAF.Data;
using FormulariosUAF.Hubs;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuestPDF.Infrastructure;
using Serilog;
using System.Threading.RateLimiting;

QuestPDF.Settings.License = LicenseType.Community;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/uaf-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// ── Base de datos ──────────────────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));


// ── Identity ───────────────────────────────────────────────────────────────
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccesoDenegado";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
});

// ── Almacenamiento de archivos (seleccionable por config) ──────────────────
var storageProvider = builder.Configuration["FileStorage:Provider"] ?? "Local";
if (storageProvider.Equals("AzureBlob", StringComparison.OrdinalIgnoreCase))
    builder.Services.AddScoped<IFileStorageService, AzureBlobStorageService>();
else
    builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

// ── Email (Fase 2) ─────────────────────────────────────────────────────────
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// ── Firma electrónica ──────────────────────────────────────────────────────
builder.Services.AddScoped<IElectronicSignatureService, SimpleElectronicSignatureService>();

// ── Exportación ZIP ────────────────────────────────────────────────────────
builder.Services.AddScoped<IExpedienteExportService, ExpedienteExportService>();

// ── Notificaciones (preparado para SignalR) ────────────────────────────────
builder.Services.AddScoped<INotificationService, NotificationService>();

// ── Otros servicios core ───────────────────────────────────────────────────
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRequestService, RequestService>();

// ── Carpeta tributaria ─────────────────────────────────────────────────────
builder.Services.AddScoped<ITaxFolderTextExtractor, PdfTextExtractorService>();
builder.Services.AddScoped<ITaxFolderAnalysisService, TaxFolderAnalysisService>();

// ── Búsqueda de empresa en base de producción (SP, solo lectura) ────────────
builder.Services.AddScoped<IEmpresaProduccionService, EmpresaProduccionService>();

// ── Proveedor de datos de empresa (configurable) ───────────────────────────
var companyProvider = builder.Configuration["CompanyDataProvider:Provider"] ?? "Manual";
if (companyProvider.Equals("Sii", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddHttpClient<ICompanyDataProvider, SiiCompanyDataProvider>();
}
else if (companyProvider.Equals("SimpleApi", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddHttpClient<ICompanyDataProvider, SimpleApiCompanyDataProvider>();
}
else
{
    builder.Services.AddScoped<ICompanyDataProvider, ManualCompanyDataProvider>();
}

// ── SignalR ────────────────────────────────────────────────────────────────
builder.Services.AddSignalR();

// ── Sesión para flujo cliente externo ──────────────────────────────────────
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = ".UAF.Session";
});

// ── Rate Limiting ──────────────────────────────────────────────────────────
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("ClientEndpoints", opt =>
    {
        opt.PermitLimit = 20;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 5;
    });
});

builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

// ── Razor Pages ────────────────────────────────────────────────────────────
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Vendedor",     "VendedorPolicy");
    options.Conventions.AuthorizeFolder("/Cumplimiento", "CumplimientoPolicy");
    options.Conventions.AuthorizeFolder("/Admin",        "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Interno",      "InternoPolicy");
    options.Conventions.AllowAnonymousToFolder("/Cliente");
    options.Conventions.AllowAnonymousToFolder("/Account");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AuthorizeFolder("/Api", "InternoPolicy");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy",        p => p.RequireRole("Administrador"));
    options.AddPolicy("VendedorPolicy",     p => p.RequireRole("Administrador", "Vendedor"));
    options.AddPolicy("CumplimientoPolicy", p => p.RequireRole("Administrador", "Cumplimiento", "Revisor"));
    options.AddPolicy("InternoPolicy",      p => p.RequireRole("Administrador", "Vendedor", "Cumplimiento", "Revisor", "SoloLectura"));
    options.AddPolicy("ExportPolicy",       p => p.RequireRole("Administrador", "Cumplimiento"));
});

var app = builder.Build();

// ── Seed ───────────────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    try { await SeedData.InitializeAsync(scope.ServiceProvider); }
    catch (Exception ex) { Log.Error(ex, "Error al inicializar la base de datos"); }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapHub<NotificationHub>("/hubs/notificaciones").RequireAuthorization();

app.Run();
