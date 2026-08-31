using _01_intro.Data;
using _01_intro.Data.Initializer;
using _01_intro.Repositories;
using _01_intro.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pv521_shop.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

// Add dbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt => 
{
    var connectionString = builder.Configuration.GetConnectionString("localDb");
    opt.UseNpgsql(connectionString);
});

// Add identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;

    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Add repositories
// Створює ваш клас у єдиному екземплярі
//builder.Services.AddSingleton<CategoryRepository>();

// Створює клас кожен раз коли він запитується
//builder.Services.AddTransient<CategoryRepository>();

// Створює клас після отримання запиту та видаляє після відправлення відповіді
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// For identity ui
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seeder
await app.SeedAsync();

app.Run();