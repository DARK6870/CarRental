using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using MediatR;
using WEB.Models;
using MediatR.Registration;
using LazyCache;    
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));

//-----------------------------------------//
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddLazyCache();

builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.ViewLocationFormats.Clear();
    o.ViewLocationFormats.Add("/Pages/Home/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Cars/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Account/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/CarType/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/DriveType/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Fuel/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Transmission/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/OrderPage/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/CarBody/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Reservation/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/Shared/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Pages/{0}" + RazorViewEngine.ViewExtension);
});


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

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "carFallback",
		pattern: "Car/{*path}",
		defaults: new { controller = "Car", action = "Cars" });

	endpoints.MapControllerRoute(
		name: "accountFallback",
		pattern: "Account/{*path}",
		defaults: new { controller = "Account", action = "Profile" });
    
    endpoints.MapControllerRoute(
		name: "carsFallback",
		pattern: "Cars/{*path}",
		defaults: new { controller = "Cars", action = "IndexCar" });
	
	endpoints.MapControllerRoute(
		name: "cartypesFallback",
		pattern: "CarTypes/{*path}",
		defaults: new { controller = "CarTypes", action = "IndexCarType" });
	
	endpoints.MapControllerRoute(
		name: "transmissionsFallback",
		pattern: "Transmissions/{*path}",
		defaults: new { controller = "Transmissions", action = "IndexTransmission" });
	
	endpoints.MapControllerRoute(
		name: "reservationsFallback",
		pattern: "Reservations/{*path}",
		defaults: new { controller = "Reservations", action = "IndexRes" });
	
	endpoints.MapControllerRoute(
		name: "fuelFallback",
		pattern: "Fuels/{*path}",
		defaults: new { controller = "Fuels", action = "IndexFuel" });
	
	endpoints.MapControllerRoute(
		name: "drive_typeFallback",
		pattern: "Drive_type/{*path}",
		defaults: new { controller = "Drive_type", action = "IndexDriveType" });
	
	endpoints.MapControllerRoute(
		name: "bodytypesFallback",
		pattern: "BodyTypes/{*path}",
		defaults: new { controller = "BodyTypes", action = "IndexCarBody" });	

	endpoints.MapControllerRoute(
		name: "ordersFallback",
		pattern: "Orders/{*path}",
		defaults: new { controller = "Orders", action = "IndexOrder" });

	endpoints.MapFallbackToController("Index", "Home");
});




app.Run();