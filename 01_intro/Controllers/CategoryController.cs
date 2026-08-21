using _01_intro.Data;
using _01_intro.Models;
using _01_intro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM viewModel)
        {
            var category = new Category
            {
                Name = viewModel.Name!,
                Image = viewModel.Image,
                Description = viewModel.Description
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new CategoryUpdateVM
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateVM viewModel)
        {
            var category = new Category
            {
                Id = viewModel.Id,
                Image = viewModel.Image,
                Name = viewModel.Name!,
                Description = viewModel.Description
            };

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

                if(category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
