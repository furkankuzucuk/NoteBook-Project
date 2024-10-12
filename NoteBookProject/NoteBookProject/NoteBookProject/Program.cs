using Microsoft.EntityFrameworkCore;
using NoteBookProject.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC'yi ekliyoruz
builder.Services.AddControllersWithViews();

// Session deste�ini ekliyoruz
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // �erezlere JavaScript �zerinden eri�ilemez
    options.Cookie.IsEssential = true; // �erez, GDPR i�in gerekli olarak i�aretlenir
});

// Veritaban� ba�lam�n� ekliyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Ortam�n geli�tirme ortam� olup olmad���n� kontrol ediyoruz
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security (HSTS) kullan�m�
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'ye y�nlendirir
app.UseStaticFiles(); // wwwroot klas�r�ndeki statik dosyalar� sunar

app.UseRouting(); // Uygulama i�in routing (y�nlendirme) kullan�m�

app.UseSession(); // Session middleware'ini ekler

app.UseAuthorization(); // Yetkilendirme middleware'ini ekler

// Varsay�lan route yap�land�rmas�n� ayarl�yoruz
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamay� �al��t�r�yoruz
app.Run();
