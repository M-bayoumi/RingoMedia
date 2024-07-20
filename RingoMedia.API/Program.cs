using Hangfire;
using Microsoft.EntityFrameworkCore;
using RingoMedia.BLL.Managers.Departments;
using RingoMedia.BLL.Managers.Mails;
using RingoMedia.DAL.Common.Settings;
using RingoMedia.DAL.Data.Context;
using RingoMedia.DAL.Repos.DepartmentRepo;
using RingoMedia.DAL.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Database
var constr = builder.Configuration.GetConnectionString("constr");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(constr);
});
builder.Services.AddControllersWithViews();

// Repos Services
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Managers
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(constr));
builder.Services.AddHangfireServer();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure Mail Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHangfireDashboard();
app.UseHangfireServer();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departments}/{action=Index}/{id?}");

app.Run();
