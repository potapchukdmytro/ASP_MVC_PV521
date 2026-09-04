using _01_intro.Services;
using Microsoft.AspNetCore.Mvc;

namespace _01_intro.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productId)
        {
            CartService.AddToCart(HttpContext.Session, productId);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            CartService.RemoveFromCart(HttpContext.Session, productId);
            return RedirectToAction("Index", "Product");
        }
    }
}
