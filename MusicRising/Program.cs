using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Data;
using MusicRising.Data.Services;
using MusicRising.Helpers;

DockerHelper.StartContainer("MariaDB");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MariaDB") ??
                       throw new InvalidOperationException("Connection string 'MariaDB' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Build the services to in the application
builder.Services.AddScoped<IShowsService, ShowsService>();
builder.Services.AddScoped<IBandsService, BandsService>();
builder.Services.AddScoped<IVenuesService, VenuesService>();
builder.Services.AddScoped<IRatingsService, RatingsService>();
builder.Services.AddScoped<IPromoItemsService, PromoItemsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Shows}/{action=Index}");
app.MapRazorPages();

// Seed the database
SeedDatabase(app);

app.Run();

void SeedDatabase(IHost app)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var bandsService = serviceScope.ServiceProvider.GetRequiredService<IBandsService>();
        var venuesService = serviceScope.ServiceProvider.GetRequiredService<IVenuesService>();
        var showsService = serviceScope.ServiceProvider.GetRequiredService<IShowsService>();

        // Check if users already exist
        if (!userManager.Users.Any(u => u.UserName == "admin"))
        {
            var seeder = new DataSeeder(userManager, bandsService, venuesService, showsService);
            seeder.SeedData().Wait();
        }
        
    }
}
