using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eLearningAutomotiveWebSite.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<eLearningAutomotiveWebSiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("eLearningAutomotiveWebSiteContext") ?? throw new InvalidOperationException("Connection string 'eLearningAutomotiveWebSiteContext' not found.")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<eLearningAutomotiveWebSiteContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    //Location for your Custom Access Denied Page
    //options.AccessDeniedPath = "Users/AccessDenied";

    //Location for your Custom Login Page
    options.LoginPath = "/Users/Login";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
