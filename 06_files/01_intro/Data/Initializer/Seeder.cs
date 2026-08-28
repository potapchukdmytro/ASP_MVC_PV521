using _01_intro.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace _01_intro.Data.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            await dbContext.Database.MigrateAsync();

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
