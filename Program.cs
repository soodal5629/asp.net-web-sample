using HelloAspMVC.Data;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services;
using DataAccessLayer.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // 뷰가 있는 컨트롤러가 웹 애플리케이션에 연결됨

// 생성된 객체가 없으면 필요할 때마다 새로 생성 및 소멸해줌(의존성 주입과 관련)
builder.Services.AddTransient<ILoginService, LoginService>(); 
builder.Services.AddTransient<ILoginMapper, LoginMapper>();
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
