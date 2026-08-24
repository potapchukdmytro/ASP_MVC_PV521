using Microsoft.AspNetCore.Mvc;

namespace _01_intro.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();             
        }
    }
}
