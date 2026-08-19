using _01_intro.Data;
using _01_intro.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
