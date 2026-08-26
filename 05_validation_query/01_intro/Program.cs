using _01_intro;
using _01_intro.Data;
using _01_intro.Data.Initializer;
using _01_intro.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add dbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt => 
{
    var connectionString = builder.Configuration.GetConnectionString("localDb");
    opt.UseNpgsql(connectionString);
});

// Add repositories
// Створює ваш клас у єдиному екземплярі
//builder.Services.AddSingleton<CategoryRepository>();

// Створює клас кожен раз коли він запитується
//builder.Services.AddTransient<CategoryRepository>();

// Створює клас після отримання запиту та видаляє після відправлення відповіді
builder.Services.AddScoped<CategoryRepository>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seeder
await app.SeedAsync();

app.Run();