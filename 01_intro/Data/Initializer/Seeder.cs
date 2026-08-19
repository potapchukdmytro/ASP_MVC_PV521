using _01_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Data.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();

            // Categories
            if (!await dbContext.Categories.AnyAsync())
            {
                List<Category> categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Процесори",
                        Description = "Центральні процесори для настільних ПК та серверів."
                    },
                    new Category
                    {
                        Name = "Відеокарти",
                        Description = "Графічні адаптери для ігор, роботи з графікою та штучного інтелекту."
                    },
                    new Category
                    {
                        Name = "Материнські плати",
                        Description = "Основні плати для підключення всіх компонентів комп'ютера."
                    },
                    new Category
                    {
                        Name = "Оперативна пам'ять",
                        Description = "Модулі пам'яті RAM (DDR4, DDR5) для швидкого доступу до даних."
                    },
                    new Category
                    {
                        Name = "Накопичувачі SSD",
                        Description = "Твердотільні диски для зберігання інформації."
                    },
                    new Category
                    {
                        Name = "Накопичувачі HDD",
                        Description = "Жорсткі диски для зберігання інформації."
                    },
                    new Category
                    {
                        Name = "Накопичувачі M.2",
                        Description = "Твердотільні швидкі диски для зберігання інформації."
                    },
                    new Category
                    {
                        Name = "Блоки живлення",
                        Description = "Пристрої для забезпечення стабільного живлення всіх компонентів системи."
                    },
                    new Category
                    {
                        Name = "Корпуси",
                        Description = "Корпуси для ПК різних форматів з підтримкою систем охолодження."
                    },
                    new Category
                    {
                        Name = "Монітори",
                        Description = "Екрани та дисплеї для комп'ютерів з різною частотою оновлення та роздільною здатністю."
                    },
                    new Category
                    {
                        Name = "Системи охолодження",
                        Description = "Повітряні кулери та системи рідинного охолодження для процесорів."
                    }
                };

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
