using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Infrastructure.DataBase.Repositories.EF.BaseData;
using App.Infrastructure.DataBase.Repositories.EF.User;
using App.Infrastructure.DataBase.Repositories.EF.Work;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region BaseDataRepositories

builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddScoped<IStatusCommandRepository, StatusCommandRepository>();
builder.Services.AddScoped<IStatusQueryRepository, StatusQueryRepository>();
builder.Services.AddScoped<ICommentCommandRepository, OpinionCommandRepository>();
builder.Services.AddScoped<ICommentQueryRepository, OpinionQueryRepository>();
builder.Services.AddScoped<IFileCommandRepository, FileCommandRepository>();
builder.Services.AddScoped<IFileQueryRepository, FileQueryRepository>();

#endregion

#region WorkRepositories

builder.Services.AddScoped<IExpertSkillCommandRepository, ExpertSkillCommandRepository>();
builder.Services.AddScoped<IExpertSkillQueryRepository, ExpertSkillQueryRepository>();
builder.Services.AddScoped<IExpertSuggestCommandRepository, ExpertSuggestCommandRepository>();
builder.Services.AddScoped<IExpertSuggestQueryRepository, ExpertSuggestQueryRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IOrderTagCommandRepository, OrderTagCommandRepository>();
builder.Services.AddScoped<IOrderTagQueryRepository, OrderTagQueryRepository>();
builder.Services.AddScoped<ISkillCommandRepository, SkillCommandRepository>();
builder.Services.AddScoped<ISkillQueryRepository, SkillQueryRepository>();
builder.Services.AddScoped<ISkillTagGroupCommandRepository, SkillTagGroupCommandRepository>();
builder.Services.AddScoped<ISkillTagGroupQueryRepository, SkillTagGroupQueryRepository>();
builder.Services.AddScoped<ITagCommandRepository, TagCommandRepository>();
builder.Services.AddScoped<ITagQueryRepository, TagQueryRepository>();
builder.Services.AddScoped<ITagGroupCommandRepository, TagGroupCommandRepository>();
builder.Services.AddScoped<ITagGroupQueryRepository, TagGroupQueryRepository>();

#endregion

#region UserRepositories

builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<IExpertCommandRepository, ExpertCommandRepository>();
builder.Services.AddScoped<IExpertQueryRepository, ExpertQueryRepository>();

#endregion


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    });


app.Run();
