using _01_intro.Data;
using _01_intro.Models;
using _01_intro.Repositories;
using _01_intro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        private string? GetPreviewImage(Product product)
        {
            string? name = product.Images.FirstOrDefault(i => i.IsPreview)?.Name;
            return name != null ? $"{product.Id}/{name}" : null;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IQueryable<Product> models = _productRepository.Products.Include(p => p.Images);


            // Pagination logic
            int count = models.Count();
            int pageSize = 18;
            int pages = (int)Math.Ceiling((double)count / pageSize);

            var products = await models.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Map products to view models
            var viewModels = products.Select(p => 
                new ProductVM
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category,
                    Preview = GetPreviewImage(p)
                });

            var vm = new CatalogVM
            {
                Products = viewModels,
                page = page,
                pages = pages
            };

            return View(vm);
        }

        private IEnumerable<SelectListItem> CategorySelectItems()
        {
            return _categoryRepository.Categories.Select(c =>
                    new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductCreateVM
            {
                Categories = CategorySelectItems()
            };

            return View(viewModel);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = CategorySelectItems();
                return View(viewModel);
            }

            var result = await _productRepository.CreateAsync(viewModel);
            if(result == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                viewModel.Categories = CategorySelectItems();
                ModelState.AddModelError("Name", result);
                return View(viewModel);
            }
        }
    }
}
