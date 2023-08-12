using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.Infrastructure.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CraftBuddyDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
})
	.AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<CraftBuddyDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/User/Login";
	options.AccessDeniedPath = "/Home/Error/401";
});

builder.Services
	.AddControllersWithViews()
	.AddMvcOptions(options =>
	{
		options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
		options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
	});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IWorkshopService, WorkshopService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

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
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	endpoints.MapDefaultControllerRoute();
});

app.MapRazorPages();

RolesInitializer.Initialize(app.Services);

app.Run();
