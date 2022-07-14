



using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectionString"));
});

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(option =>
 {
     option.SignIn.RequireConfirmedEmail = false;
     option.SignIn.RequireConfirmedPhoneNumber = false;
     option.SignIn.RequireConfirmedAccount = false;

 }).AddEntityFrameworkStores<AppDbContext>();












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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
