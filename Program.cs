using MiniShopManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// Add services to the container.

// Add session and distributed memory cache (once)
builder.Services.AddDistributedMemoryCache();

=======
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// إضافة خدمات MVC
builder.Services.AddControllersWithViews();

// إضافة قاعدة البيانات SQLite
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة خدمات الـ Session
builder.Services.AddDistributedMemoryCache();
>>>>>>> 78c11f5455149c2718da44cba7db7d7daa4d5b08
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

<<<<<<< HEAD
// Add MVC controllers with views
builder.Services.AddControllersWithViews();

// Add your SQLite DB context
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

=======
var app = builder.Build();

// Configure the HTTP request pipeline.
>>>>>>> 78c11f5455149c2718da44cba7db7d7daa4d5b08
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
<<<<<<< HEAD
=======

// ✅ خدمة الملفات من wwwroot
>>>>>>> 78c11f5455149c2718da44cba7db7d7daa4d5b08
app.UseStaticFiles();

app.UseRouting();

<<<<<<< HEAD
app.UseSession();  // Must be before UseAuthorization and MapControllerRoute

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
=======
// ✅ تفعيل Session قبل Authorization
app.UseSession();

app.UseAuthorization();

// Map controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
>>>>>>> 78c11f5455149c2718da44cba7db7d7daa4d5b08

app.Run();
