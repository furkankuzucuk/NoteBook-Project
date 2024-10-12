using Microsoft.EntityFrameworkCore;
using NoteBookProject.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC'yi ekliyoruz
builder.Services.AddControllersWithViews();

// Session desteðini ekliyoruz
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true; // Çerezlere JavaScript üzerinden eriþilemez
    options.Cookie.IsEssential = true; // Çerez, GDPR için gerekli olarak iþaretlenir
});

// Veritabaný baðlamýný ekliyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Ortamýn geliþtirme ortamý olup olmadýðýný kontrol ediyoruz
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security (HSTS) kullanýmý
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'ye yönlendirir
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyalarý sunar

app.UseRouting(); // Uygulama için routing (yönlendirme) kullanýmý

app.UseSession(); // Session middleware'ini ekler

app.UseAuthorization(); // Yetkilendirme middleware'ini ekler

// Varsayýlan route yapýlandýrmasýný ayarlýyoruz
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamayý çalýþtýrýyoruz
app.Run();
