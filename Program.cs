using DrivingLog.Models;
using DrivingLog.Services.Settings;
using DrivingLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DrivingLog.Services.LogEntryService;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Prod")), ServiceLifetime.Singleton);
}

builder.Services.AddSingleton<ILogEntryService, LogEntryService>();
builder.Services.AddSingleton<ISettingsService, SettingsService>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=LogEntry}/{action=LogEntry}/{id?}");
    endpoints.MapControllerRoute(
        name: "monthRoute",
        pattern: "{controller=LogEntry}/{action=GetLogEntriesByDate}/{monthYear?}");
    endpoints.MapRazorPages();
});

var context = app.Services.GetRequiredService<AppDbContext>();

await context.Database.MigrateAsync();

if (context.Settings.FirstOrDefault() is null)
{
    await context.Settings.AddAsync(new Setting { HourlyRate = 12 });
    await context.SaveChangesAsync();
}

app.MapRazorPages();

app.Run();
