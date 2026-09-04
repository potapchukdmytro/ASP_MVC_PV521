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
        
        private int[] GetPages(int page, int count)
        {
            if(count <= 5)
            {
                return Enumerable.Range(1, count).ToArray();
            }

            if (page <= 3)
            {
                return [1, 2, 3, 4, -1, count];
            }

            if(page >= count - 2)
            {
                return [1, -1, count - 3, count - 2, count -1, count,];
            }

            return [1, -1, page - 1, page, page + 1, -1, count];
        }

        public async Task<IActionResult> Index(int page = 1, string? category = null)
        {
            IQueryable<Product> models = _productRepository.Products
                .Include(p => p.Images)
                .Include(p => p.Category);

            if(!string.IsNullOrEmpty(category))
            {
                models = models.Where(p => p.Category!.Name.ToLower() == category.ToLower());
            }

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
                Page = page,
                LastPage = pages,
                Category = category,
                Categories = await _categoryRepository.Categories.ToListAsync(),
                Pages = GetPages(page, pages)
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
