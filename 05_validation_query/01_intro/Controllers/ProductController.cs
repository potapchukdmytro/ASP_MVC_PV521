using _01_intro.Data;
using _01_intro.Models;
using _01_intro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _01_intro.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            IQueryable<Product> products = _context.Products;

            int count = products.Count();
            int pageSize = 18;
            int pages = (int)Math.Ceiling((double)count / pageSize);

            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            var vm = new CatalogVM
            {
                Products = products,
                page = page,
                pages = pages
            };

            return View(vm);
        }
    }
}
