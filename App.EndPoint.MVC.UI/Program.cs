using App.Domain.AppServices.BaseData;
using App.Domain.AppServices.Work;
using App.Domain.Core.BaseData.Contracts.AppServices;
using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.BaseData.Contracts.Services;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Services.BaseData;
using App.Domain.Services.Work;
using App.Infrastructure.DataBase.Repositories.EF.BaseData;
using App.Infrastructure.DataBase.Repositories.EF.User.Admin;
using App.Infrastructure.DataBase.Repositories.EF.Work;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region BaseDataRepositories


builder.Services.AddScoped<IStatusCommandRepository, StatusCommandRepository>();
builder.Services.AddScoped<IStatusQueryRepository, StatusQueryRepository>();

builder.Services.AddScoped<IFileCommandRepository, FileCommandRepository>();
builder.Services.AddScoped<IFileQueryRepository, FileQueryRepository>();

#endregion

#region WorkRepositories
builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddScoped<IExpertCategoryCommandRepository, ExpertCategoryCommandRepository>();
builder.Services.AddScoped<IExpertCategoryQueryRepository, ExpertCategoryQueryRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IOrderFileCommandRepository, OrderFileCommandRepository>();
builder.Services.AddScoped<IOrderFileQueryRepository, OrderFileQueryRepository>();
builder.Services.AddScoped<IServiceCommandRepository, ServiceCommandRepository>();
builder.Services.AddScoped<IServiceQueryRepository, ServiceQueryRepository>();
builder.Services.AddScoped<IServiceCommentCommandRepository, ServiceCommentCommandRepository>();
builder.Services.AddScoped<IServiceCommentQueryRepository, ServiceCommentQueryRepository>();
builder.Services.AddScoped<ISuggestCommandRepository, SuggestCommandRepository>();
builder.Services.AddScoped<ISuggestQueryRepository, SuggestQueryRepository>();
builder.Services.AddScoped<IServiceFileCommandRepository, ServiceFileCommandRepository>();
builder.Services.AddScoped<IServiceFileQueryRepository, ServiceFileQueryRepository>();

#endregion

#region UserRepositories

builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

#endregion

#region WorkServices

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ICommentService, CommentService>();

#endregion

#region BaseDataServices

builder.Services.AddScoped<IStatusService, StatusService>();

#endregion

#region WorkAppServices

builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IServiceAppService, ServcieAppServcie>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();

#endregion

#region BaseDataAppServices

builder.Services.AddScoped<IStatusAppServcie, StatusAppService>();


#endregion


builder.Services.AddControllersWithViews();





builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectionString"));
});

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(option =>
 {
     option.SignIn.RequireConfirmedEmail = false;
     option.SignIn.RequireConfirmedPhoneNumber = false;
     option.SignIn.RequireConfirmedAccount = false;
     
     option.Password.RequireUppercase = false;
     option.Password.RequireDigit = false;
     option.Password.RequireNonAlphanumeric = false;
     option.Password.RequireLowercase = false;

 }).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(x => x.LoginPath = "/Admin/Account/Login");












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

app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "areas",
//        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
//    });


app.Run();
