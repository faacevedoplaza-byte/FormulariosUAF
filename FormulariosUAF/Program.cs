using System.Threading.RateLimiting;
using FormulariosUAF.Data;
using FormulariosUAF.Models.Domain;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Serilog;

QuestPDF.Settings.License = LicenseType.Community;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/uaf-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = ".UAF.Session";
});

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

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Vendedor", "VendedorPolicy");
    options.Conventions.AuthorizeFolder("/Cumplimiento", "CumplimientoPolicy");
    options.Conventions.AuthorizeFolder("/Admin", "AdminPolicy");
    options.Conventions.AllowAnonymousToFolder("/Cliente");
    options.Conventions.AllowAnonymousToFolder("/Account");
    options.Conventions.AllowAnonymousToPage("/Index");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", p => p.RequireRole("Administrador"));
    options.AddPolicy("VendedorPolicy", p => p.RequireRole("Administrador", "Vendedor"));
    options.AddPolicy("CumplimientoPolicy", p => p.RequireRole("Administrador", "Cumplimiento"));
});

var app = builder.Build();

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

app.Run();
