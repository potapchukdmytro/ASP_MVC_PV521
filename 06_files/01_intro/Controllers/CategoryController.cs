using _01_intro.Models;
using _01_intro.Repositories;
using _01_intro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _01_intro.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.Categories;

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
            // Validation
            //ModelState.AddModelError("Name", "Тестова помилка");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _categoryRepository.CreateAsync(viewModel);

            if(result == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var category = await _categoryRepository.GetByIdAsync((int)id);

            if (category == null)
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _categoryRepository.UpdateAsync(viewModel);

            if(result == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(result);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var result =  await _categoryRepository.DeleteAsync((int)id);
                if(result != null)
                {
                    return BadRequest(result);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
