using Microsoft.AspNetCore.Mvc;

namespace _01_intro.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Catalog()
        {
            return View();
        }
    }
}
