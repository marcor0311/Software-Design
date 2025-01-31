using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.AccesoDatos.Repositorio;
using EFoodCommerce.Utilidades;
using EFoodCommerce.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using EFoodCommerce.AccesoDatos;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
builder.Services.AddTransient<IUsuarioActual, UsuarioActual>();
var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Error/{0}");

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development environment");
    app.UseMigrationsEndPoint();
    //app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Excepciones>();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=EFoodCommerce}/{controller=Home}/{action=Index}/{codigo?}");
app.MapRazorPages();

app.Run();
