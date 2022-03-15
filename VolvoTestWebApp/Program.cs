using Microsoft.EntityFrameworkCore;
using VolvoTestWebApp.Data;
using VolvoTestWebApp.Data.Repositories;
using VolvoTestWebApp.Data.Repositories.Abstractions;
using VolvoTestWebApp.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VolvoTestWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VolvoTestWebAppContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVolvoTruckRepository, VolvoTruckRepository>();

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

app.MigrateDatabase()
    .Run();
