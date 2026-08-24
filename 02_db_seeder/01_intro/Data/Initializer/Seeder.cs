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
                        Description = "Центральні процесори для настільних ПК та серверів.",
                        Image = "https://s.ek.ua/posts/files/6001/wide_pic.jpg"
                    },
                    new Category
                    {
                        Name = "Відеокарти",
                        Description = "Графічні адаптери для ігор, роботи з графікою та штучного інтелекту.",
                        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMrlqSfU0wP5Pq8f20kzIUBgPSo9jWYdLqNJH_SyHhrJel18he8go2tsOH&s=10"
                    },
                    new Category
                    {
                        Name = "Материнські плати",
                        Description = "Основні плати для підключення всіх компонентів комп'ютера.",
                        Image = "https://s.ek.ua/posts/files/6619/wide_pic.jpg"
                    },
                    new Category
                    {
                        Name = "Оперативна пам'ять",
                        Description = "Модулі пам'яті RAM (DDR4, DDR5) для швидкого доступу до даних.",
                        Image = "https://ipkey.com.ua/media/k2/items/cache/0afa57c41887ce47355dc00226c6156f_XL.jpg"
                    },
                    new Category
                    {
                        Name = "Накопичувачі SSD",
                        Description = "Твердотільні диски для зберігання інформації.",
                        Image = "https://hotline.ua/img/uploads/clients/5d8b721a44df5.jpg"
                    },
                    new Category
                    {
                        Name = "Накопичувачі HDD",
                        Description = "Жорсткі диски для зберігання інформації.",
                        Image = "https://artline.ua/storage/images/editor/editor_1667650143803718_0.webp"
                    },
                    new Category
                    {
                        Name = "Накопичувачі M.2",
                        Description = "Твердотільні швидкі диски для зберігання інформації.",
                        Image = "https://www.kingspec.com/uploads/image/66f0bf6010378.png"
                    },
                    new Category
                    {
                        Name = "Блоки живлення",
                        Description = "Пристрої для забезпечення стабільного живлення всіх компонентів системи.",
                        Image = "https://marketing.brain.com.ua/static/articles_icon/7394.webp"
                    },
                    new Category
                    {
                        Name = "Корпуси",
                        Description = "Корпуси для ПК різних форматів з підтримкою систем охолодження.",
                        Image = "https://s.ek.ua/posts/files/5655/wide_pic.jpg"
                    },
                    new Category
                    {
                        Name = "Монітори",
                        Description = "Екрани та дисплеї для комп'ютерів з різною частотою оновлення та роздільною здатністю.",
                        Image = "https://denika.ua/image/cache/catalog/blog/kak-vibrat_monitor-912x466.png"
                    },
                    new Category
                    {
                        Name = "Системи охолодження",
                        Description = "Повітряні кулери та системи рідинного охолодження для процесорів.",
                        Image = "https://s.ek.ua/posts/files/6033/wide_pic.jpg"
                    }
                };

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
