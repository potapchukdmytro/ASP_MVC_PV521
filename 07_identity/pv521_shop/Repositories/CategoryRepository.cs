using _01_intro.Data;
using _01_intro.Models;
using _01_intro.Services;
using _01_intro.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ImageService _imageService;
        private readonly string _imagesPath;

        public CategoryRepository(ApplicationDbContext context, IWebHostEnvironment environment, ImageService imageService)
        {
            _context = context;
            _environment = environment;
            _imageService = imageService;

            string root = _environment.WebRootPath;
            _imagesPath = Path.Combine(root, "images", "categories");
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
                Name = vm.Name
            };

            // SaveImage
            if(vm.Image != null)
            {
                model.Image = await _imageService.SaveImageAsync(vm.Image, _imagesPath);

                //if (model.Image != null)
                //{
                //    string previewPath = Path.Combine(_imagesPath, "preview", model.Image);
                //    await _imageService.PreviewImage(vm.Image, previewPath);
                //}
            }

            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> UpdateAsync(CategoryUpdateVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Name))
            {
                return "Назва категорії є обов'язковою";
            }

            if (await Categories.AnyAsync(c => c.Name.ToLower() == vm.Name.ToLower() && c.Id != vm.Id))
            {
                return $"Категорія '{vm.Name}' вже існує";
            }

            var model = await GetByIdAsync(vm.Id);

            if(model == null)
            {
                return $"Категорія за id '{vm.Id}' не знайдена";
            }

            model.Description = vm.Description;
            model.Name = vm.Name;

            if(vm.Image != null)
            {
                if(model.Image != null)
                {
                    string imagePath = Path.Combine(_imagesPath, model.Image);
                    _imageService.DeleteImage(imagePath);
                }

                model.Image = await _imageService.SaveImageAsync(vm.Image, _imagesPath);
            }

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var model = await GetByIdAsync(id);

            if(model == null)
            {
                return $"Категорія за id '{id}' не знайдена";
            }

            string? modelImage = model.Image;

            _context.Categories.Remove(model);
            int res = await _context.SaveChangesAsync();

            if (modelImage != null && res != 0)
            {
                string imagePath = Path.Combine(_imagesPath, modelImage);
                _imageService.DeleteImage(imagePath);
            }

            return null;
        }
    }
}
