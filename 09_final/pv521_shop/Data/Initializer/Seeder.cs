using _01_intro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pv521_shop.Data;
using System.Text.Json;

namespace _01_intro.Data.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            await dbContext.Database.MigrateAsync();

            // Roles and Users
            if (!roleManager.Roles.Any())
            {
                var adminRole = new IdentityRole { Name = "admin" };
                var userRole = new IdentityRole { Name = "user" };

                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);

                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(admin, "qwerty");

                await userManager.AddToRoleAsync(admin, "admin");
            }

            // Categories
            if (!await dbContext.Categories.AnyAsync())
            {
                string root = env.WebRootPath;
                string path = Path.Combine(root, "seedData", "categoriesAndProducts.json");
                string json = await File.ReadAllTextAsync(path);

                var data = JsonSerializer.Deserialize<List<Category>>(json);
                if(data != null)
                {
                    await dbContext.Categories.AddRangeAsync(data);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
