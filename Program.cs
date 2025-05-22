using HelloAspMVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // 뷰가 있는 컨트롤러가 웹 애플리케이션에 연결됨

// db
builder.Services.AddDbContext<SchoolContext>
(   options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    // pattern: "{controller=Home}/{action=Index}/{id?}")
    pattern: "{controller=Home}/{action=tempIndex}"
    )
    .WithStaticAssets();

// app.MapControllerRoute(
//     name: "test",
//     pattern: "{controller=Test}/{action=test}"
//     )
//     .WithStaticAssets();

app.Run();
