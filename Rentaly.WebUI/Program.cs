var builder = WebApplication.CreateBuilder(args);

// Servisleri container'a ekle.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP istek hattını yapılandır.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Varsayılan HSTS süresi 30 gündür. Üretim senaryolarında değiştirmek isteyebilirsiniz: https://aka.ms/aspnetcore-hsts
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();