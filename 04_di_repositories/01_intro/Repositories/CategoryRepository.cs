using _01_intro.Data;
using _01_intro.Models;
using _01_intro.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public int Value { get; set; } = 1;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories.AsNoTracking();

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            return await GetByNameAsync(name) != null;
        }

        public async Task<string?> CreateAsync(CategoryCreateVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Name))
            {
                return "Назва категорії є обов'язковою";
            }

            
            if(await IsExistsAsync(vm.Name))
            {
                return $"Категорія '{vm.Name}' вже існує";
            }

            var model = new Category
            {
                Description = vm.Description,
                Name = vm.Name,
                Image = vm.Image
            };

            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
