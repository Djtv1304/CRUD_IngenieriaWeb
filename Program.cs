using CRUD_IngenieriaWeb.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAPIServiceUsuario, APIServiceUsuario>();
builder.Services.AddScoped<IAPIServiceVacante, APIServiceVacante>();

// Agregar el servicio de IActionContextAccessor aquí
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Agregar el servicio de sesión aquí
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Establecer un tiempo de espera de la sesión, por ejemplo, 40 minutos
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configurar la autenticación y autorización
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Ruta para la página de login
        options.AccessDeniedPath = "/Login/Unauthorized"; // Ruta para la página de acceso denegado
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Agregar el middleware de sesión aquí
app.UseSession();

// Agregar el middleware de autenticación y autorización aquí
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
