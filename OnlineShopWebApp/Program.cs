using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp;
using OnlineShopWebApp.Controllers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
using (var scope = builder.Build())
{
    var services = scope.Services.CreateScope();
    var userManager = services.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = services.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}
builder.Services.AddIdentity<User, IdentityRole>();
builder.Services.ConfigureApplicationCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.LoginPath = $"/{nameof(AccountController).Replace("Controller", "")}/{nameof(AccountController.Login)}";
        options.LogoutPath = $"/{nameof(AccountController).Replace("Controller", "")}/{nameof(AccountController.Logout)}";
        options.Cookie = new CookieBuilder
        {
            IsEssential = true
        };
    });
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("online_shop")));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("online_shop")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
builder.Services.AddTransient<IFavoriteDbRepository, FavoriteDbRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
builder.Services.AddSingleton<IRolesRepository, RolesInMemoryRepository>();
builder.Services.AddSingleton<IUsersManager, UsersManager>();
builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext().Enrich.WithProperty("ApplicationName", "Online Shop");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
